
using System.Numerics;
using RayTracerInAWeekend.Hitables;

namespace RayTracerInAWeekend.Materials
{
    public abstract class Material
    {
        public abstract bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered);

        public virtual Vector3 Emitted(float u, float v, Vector3 hitPoint)
        {
            return Vector3.Zero;
        }
    }

    public interface IVisible
    {
        Material Material { get; }
    }
}
