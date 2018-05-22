using System;
using System.Numerics;
using System.Threading;

namespace RayTracerInAWeekend
{
    static class VectorHelpers
    {
        private static ThreadLocal<Random> rng = new ThreadLocal<Random>(() => new Random());
        public static float RandomFloat()
        {
            return (float) rng.Value.NextDouble();
        }

        public static Vector3 GetRandomInUnitSphere()
        {
            Random rng = new Random();
            Vector3 p;
            do
            {
                p = new Vector3(2 * RandomFloat(), 2 * RandomFloat(), 2 * RandomFloat()) - Vector3.One;
            } while (p.LengthSquared() >= 1.0);

            return p;
        }

        public static Vector3 Reflect(Vector3 v, Vector3 normal)
        {
            return v - 2 * Vector3.Dot(v, normal) * normal;
        }

        public static Vector3 GetUnitVector(this Vector3 vector)
        {
            return vector / vector.Length();
        }
    }
}
