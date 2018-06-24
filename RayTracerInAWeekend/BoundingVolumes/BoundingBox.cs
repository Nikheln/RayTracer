using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using RayTracerInAWeekend.Hitables;

namespace RayTracerInAWeekend
{
    public class BoundingBox : IHitable
    {
        private static float FfMin(float a, float b) => a < b ? a : b;
        private static float FfMax(float a, float b) => a > b ? a : b;

        public readonly Vector3 Min, Max;

        public BoundingBox(Vector3 min, Vector3 max)
        {
            Min = min;
            Max = max;
        }

        public BoundingBox(params BoundingBox[] boxes)
        {
            if (boxes.Length == 0)
            {
                return;
            }
            var vectors = boxes.SelectMany(box => new List<Vector3>() { box.Min, box.Max });

            Min = new Vector3(
                vectors.Select(vec => vec.X).Min(),
                vectors.Select(vec => vec.Y).Min(),
                vectors.Select(vec => vec.Z).Min()
                );
            Max = new Vector3(
                vectors.Select(vec => vec.X).Max(),
                vectors.Select(vec => vec.Y).Max(),
                vectors.Select(vec => vec.Z).Max()
                );
        }
        
        public bool IsHitBy(Ray r, float tMin, float tMax, out HitRecord record)
        {
            record = HitableExtensions.NULL_RECORD;
            Vector3 minDiff = Vector3.Divide(Min - r.Origin, r.Direction);
            Vector3 maxDiff = Vector3.Divide(Max - r.Origin, r.Direction);

            return
                FfMin(FfMax(minDiff.X, maxDiff.X), tMax) > FfMax(FfMin(minDiff.X, maxDiff.X), tMin) &&
                FfMin(FfMax(minDiff.Y, maxDiff.Y), tMax) > FfMax(FfMin(minDiff.Y, maxDiff.Y), tMin) &&
                FfMin(FfMax(minDiff.Z, maxDiff.Z), tMax) > FfMax(FfMin(minDiff.Z, maxDiff.Z), tMin);
        }

        bool IHitable.BoundingBox(float t0, float t1, out BoundingBox box)
        {
            throw new System.NotImplementedException();
        }
    }
}
