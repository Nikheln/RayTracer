
using System.Numerics;

namespace RayTracerInAWeekend.Materials
{
    class Lambertian : IMaterial
    {
        private Vector3 Albedo;
        public Lambertian(Vector3 albedo)
        {
            Albedo = albedo;
        }

        public bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 target = hitRecord.HitPoint + hitRecord.SurfaceNormal + VectorHelpers.GetRandomInUnitSphere();
            scattered = new Ray(hitRecord.HitPoint, target - hitRecord.HitPoint);
            attenuation = Albedo;
            return true;
        }
    }
}
