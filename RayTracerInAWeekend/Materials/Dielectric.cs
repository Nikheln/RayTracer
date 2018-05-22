
using System;
using System.Numerics;

namespace RayTracerInAWeekend.Materials
{
    class Dielectric : IMaterial
    {
        private float RefractiveIndex;
        private Vector3 Albedo;

        public Dielectric(float refractiveIndex, Vector3 albedo)
        {
            RefractiveIndex = refractiveIndex;
            Albedo = albedo;
        }

        public bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            attenuation = Albedo;

            Vector3 reflected = VectorHelpers.Reflect(r.Direction, hitRecord.SurfaceNormal);
            Vector3 outwardNormal;
            float niOverNt;
            float cosine;

            if (Vector3.Dot(r.Direction, hitRecord.SurfaceNormal) > 0)
            {
                outwardNormal = -hitRecord.SurfaceNormal;
                niOverNt = RefractiveIndex;
                cosine = RefractiveIndex * Vector3.Dot(r.Direction, hitRecord.SurfaceNormal) / r.Direction.Length();
            }
            else
            {
                outwardNormal = hitRecord.SurfaceNormal;
                niOverNt = 1.0f / RefractiveIndex;
                cosine = -Vector3.Dot(r.Direction, hitRecord.SurfaceNormal) / r.Direction.Length();
            }

            float reflectProb = 1.0f;

            if (Refract(r.Direction, outwardNormal, niOverNt, out Vector3 refracted))
            {
                reflectProb = Schlick(cosine);
            }

            if (VectorHelpers.RandomFloat() < reflectProb)
            {
                scattered = new Ray(hitRecord.HitPoint, reflected);
            }
            else
            {
                scattered = new Ray(hitRecord.HitPoint, refracted);
            }
            return true;
        }

        private float Schlick(float cosine)
        {
            float r0 = (1f - RefractiveIndex) / (1f + RefractiveIndex);
            r0 *= r0;
            return r0 + (1f - r0) * (float) Math.Pow(1 - cosine, 5);
        }

        private bool Refract(Vector3 v, Vector3 surfaceNormal, float niOverNt, out Vector3 refracted)
        {
            Vector3 unitV = v / v.Length();
            float dt = Vector3.Dot(unitV, surfaceNormal);
            float discriminant = 1.0f - niOverNt * niOverNt * (1.0f - dt * dt);

            if (discriminant > 0)
            {
                refracted = niOverNt * (unitV - surfaceNormal * dt) - surfaceNormal * (float) Math.Sqrt(discriminant);
                return true;
            }
            else
            {
                refracted = Vector3.Zero;
                return false;
            }
        }
    }
}
