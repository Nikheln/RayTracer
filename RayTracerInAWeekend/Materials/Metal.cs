
using System;
using System.Numerics;
using RayTracerInAWeekend.Hitables;

namespace RayTracerInAWeekend.Materials
{
    class Metal : Material
    {
        private Vector3 Albedo;
        private readonly float Fuzz;

        public Metal(Vector3 albedo, float fuzziness)
        {
            Albedo = albedo;
            Fuzz = fuzziness;
        }

        public override bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 reflected = VectorHelpers.Reflect(r.Direction.GetUnitVector(), hitRecord.SurfaceNormal);
            scattered = new Ray(hitRecord.HitPoint, reflected + Fuzz * VectorHelpers.GetRandomInUnitSphere());
            attenuation = Albedo;
            return Vector3.Dot(scattered.Direction, hitRecord.SurfaceNormal) > 0;
        }
    }
}
