
using System;
using System.Numerics;

namespace RayTracerInAWeekend
{
    public class Ray
    {
        public Vector3 Origin, Direction;

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 PointAtParameter(float t)
        {
            return Origin + t * Direction;
        }
    }
}
