using System.Collections.Generic;
using System.Numerics;
using RayTracerInAWeekend.BoundingVolumes;
using RayTracerInAWeekend.Materials;

namespace RayTracerInAWeekend.Hitables
{
    public struct HitRecord
    {
        public float t, u, v;
        public Vector3 HitPoint;
        public Vector3 SurfaceNormal;
        public Material Material;
    }

    public interface IHitable
    {
        bool IsHitBy(Ray r, float tMin, float tMax, out HitRecord record);
        bool BoundingBox(float t0, float t1, out BoundingBox box);
    }

    public static class HitableExtensions
    {
        public static readonly HitRecord NULL_RECORD = new HitRecord();

        public static BoundingBox GetGenericBoundingBox(this IHitable hitable)
        {
            hitable.BoundingBox(float.MinValue, float.MaxValue, out BoundingBox bb);
            return bb;
        }

        public static Vector3 GetBBCenter(this IHitable hitable)
        {
            var bb = hitable.GetGenericBoundingBox();
            return new Vector3(
                (bb.Min.X + bb.Max.X) / 2,
                (bb.Min.Y + bb.Max.Y) / 2,
                (bb.Min.Z + bb.Max.Z) / 2
                );
        }
    }

    class HitableList : List<IHitable>, IHitable
    {
        private BVHNode bvhRoot;

        public bool BoundingBox(float t0, float t1, out BoundingBox box)
        {
            box = new BoundingBox(Vector3.Zero, Vector3.Zero);
            if (Count == 0)
            {
                return false;
            }

            if (!this[0].BoundingBox(t0, t1, out BoundingBox tempBox))
            {
                return false;
            }

            box = tempBox;

            for (int i = 1; i < Count; i++)
            {
                if (this[i].BoundingBox(t0, t1, out tempBox))
                {
                    box = new BoundingBox(box, tempBox);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void RebuildBvhTree()
        {
            bvhRoot = new BVHNode(ToArray());
        }

        public bool IsHitBy(Ray r, float tMin, float tMax, out HitRecord record)
        {
            return bvhRoot.IsHitBy(r, tMin, tMax, out record);
        }
    }
}
