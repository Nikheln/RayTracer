
using System;
using System.Numerics;

namespace RayTracerInAWeekend.Materials
{
    class Metal : IMaterial
    {
        private Vector3 Albedo;
        private float Fuzz;

        public Metal(Vector3 albedo, float fuzziness)
        {
            Albedo = albedo;
            Fuzz = fuzziness;
        }

        public bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 reflected = VectorHelpers.Reflect(r.Direction / r.Direction.Length(), hitRecord.SurfaceNormal);
            scattered = new Ray(hitRecord.HitPoint, reflected + Fuzz * VectorHelpers.GetRandomInUnitSphere());
            attenuation = Albedo;
            return Vector3.Dot(scattered.Direction, hitRecord.SurfaceNormal) > 0;
        }
    }
}
