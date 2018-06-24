
using System.Numerics;
using RayTracerInAWeekend.Hitables;
using RayTracerInAWeekend.Textures;

namespace RayTracerInAWeekend.Materials
{
    class Lambertian : Material
    {
        private readonly ITexture Texture;
        public Lambertian(ITexture texture)
        {
            Texture = texture;
        }

        public override bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            Vector3 target = hitRecord.HitPoint + hitRecord.SurfaceNormal + VectorHelpers.GetRandomInUnitSphere();
            scattered = new Ray(hitRecord.HitPoint, target - hitRecord.HitPoint);
            attenuation = Texture.GetValue(0, 0, hitRecord.HitPoint);
            return true;
        }
    }
}
