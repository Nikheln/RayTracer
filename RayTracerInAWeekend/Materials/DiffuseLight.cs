
using System;
using System.Numerics;
using RayTracerInAWeekend.Hitables;
using RayTracerInAWeekend.Textures;

namespace RayTracerInAWeekend.Materials
{
    public class DiffuseLight : Material
    {
        private readonly ITexture emit;

        public DiffuseLight(ITexture texture)
        {
            this.emit = texture;
        }

        public override bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            attenuation = Vector3.Zero;
            scattered = new Ray(Vector3.Zero, Vector3.Zero);
            return false;
        }

        public override Vector3 Emitted(float u, float v, Vector3 hitPoint)
        {
            return emit.GetValue(u, v, hitPoint);
        }
    }
}
