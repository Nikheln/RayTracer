
using System.Collections.Generic;
using System.Linq;
using RayTracerInAWeekend.Hitables;

namespace RayTracerInAWeekend.BoundingVolumes
{
    public class BVHNode : IHitable
    {
        public IHitable Left, Right;
        private BoundingBox _boundingBox;

        public BVHNode(params IHitable[] children)
        {
            if (children.Length == 0)
            {
                return;
            }

            AxisSelector.Axis sortingAxis = AxisSelector.GetMaxVarianceAxis(children);
            IEnumerable<IHitable> sortedChildren = children.OrderBy(child =>
            {
                var bbCenter = child.GetBBCenter();
                switch (sortingAxis)
                {
                    case AxisSelector.Axis.X:
                        return bbCenter.X;
                    case AxisSelector.Axis.Y:
                        return bbCenter.Y;
                    case AxisSelector.Axis.Z:
                        return bbCenter.Z;
                    default:
                        return 0;
                }
            });
            switch (children.Length)
            {
                case 1:
                    Left = Right = children[0];
                    break;
                case 2:
                    Left = children[0];
                    Right = children[1];
                    break;
                default:
                    int firstHalfSize = children.Length / 2;
                    Left = new BVHNode(sortedChildren.Take(firstHalfSize).ToArray());
                    Right = new BVHNode(sortedChildren.Skip(firstHalfSize).ToArray());
                    break;
            }

            _boundingBox = new BoundingBox(Left.GetGenericBoundingBox(), Right.GetGenericBoundingBox());
        }
        
        public bool BoundingBox(float t0, float t1, out BoundingBox box)
        {
            box = _boundingBox;
            return true;
        }

        public bool IsHitBy(Ray r, float tMin, float tMax, out HitRecord record)
        {
            if (_boundingBox.IsHitBy(r, tMin, tMax, out record))
            {
                bool hitLeft = Left.IsHitBy(r, tMin, tMax, out HitRecord leftRecord);
                bool hitRight = Right.IsHitBy(r, tMin, tMax, out HitRecord rightRecord);

                if (hitLeft || hitRight)
                {
                    if (hitLeft && hitRight)
                    {
                        record = leftRecord.t < rightRecord.t ? leftRecord : rightRecord;
                    }
                    else if (hitLeft)
                    {
                        record = leftRecord;
                    }
                    else if (hitRight)
                    {
                        record = rightRecord;
                    }
                    return true;
                }
            }
            return false;
        }
    }

    static class AxisSelector
    {
        internal enum Axis
        {
            X, Y, Z
        }

        internal static Axis GetMaxVarianceAxis(params IHitable[] hitables)
        {
            IEnumerable<BoundingBox> bbs = hitables.Select(hitable => hitable.GetGenericBoundingBox());
            
            double xVariance = Variance(bbs.Select(bb => (bb.Max.X + bb.Min.X) / 2).ToList());
            double yVariance = Variance(bbs.Select(bb => (bb.Max.Y + bb.Min.Y) / 2).ToList());
            double zVariance = Variance(bbs.Select(bb => (bb.Max.Z + bb.Min.Z) / 2).ToList());

            if (xVariance > yVariance && xVariance > zVariance)
            {
                return Axis.X;
            }
            else if (yVariance > zVariance)
            {
                return Axis.Y;
            }
            else
            {
                return Axis.Z;
            }
        }

        private static double Variance(IList<float> values)
        {
            double variance = 0;
            double mean = Mean(values);

            for (int i = 0; i < values.Count; i++)
            {
                variance += System.Math.Pow((values[i] - mean), 2);
            }

            return variance / mean;
        }

        private static double Mean(IList<float> values) => values.Sum() / values.Count;
    }
}
