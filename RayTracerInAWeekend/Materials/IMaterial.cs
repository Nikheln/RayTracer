
using System.Numerics;

namespace RayTracerInAWeekend.Materials
{
    public interface IMaterial
    {
        bool Scatter(Ray r, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered);
    }
}
