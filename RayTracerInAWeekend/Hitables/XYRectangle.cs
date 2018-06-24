

using System.Numerics;
using RayTracerInAWeekend.Materials;

namespace RayTracerInAWeekend.Hitables
{
    class XYRectangle : IHitable
    {
        private readonly float x0, x1, y0, y1, z;
        private readonly Material material;
        public XYRectangle(float x0, float y0, float z, float width, float height, Material material)
        {
            this.x0 = x0;
            this.x1 = x0 + width;
            this.y0 = y0;
            this.y1 = y0 + height;
            this.z = z;

            this.material = material;
            this._bb = new BoundingBox(new Vector3(x0, y0, z - 0.0001f), new Vector3(x1, y1, z + 0.0001f));
        }

        private readonly BoundingBox _bb;
        public bool BoundingBox(float t0, float t1, out BoundingBox box)
        {
            box = _bb;
            return true;
        }

        public bool IsHitBy(Ray r, float tMin, float tMax, out HitRecord record)
        {
            float t = (z - r.Origin.Z) / r.Direction.Z;
            if (t < tMin || t > tMax)
            {
                record = HitableExtensions.NULL_RECORD;
                return false;
            }

            float x = r.Origin.X + t * r.Direction.X;
            float y = r.Origin.Y + t * r.Direction.Y;
            if (x < x0 || x > x1 || y < y0 ||y > y1)
            {
                record = HitableExtensions.NULL_RECORD;
                return false;
            }

            record = new HitRecord()
            {
                u = (x - x0) / (x1 - x0),
                v = (y - y0) / (y1 - y0),
                t = t,
                Material = material,
                HitPoint = r.PointAtParameter(t),
                SurfaceNormal = new Vector3(0, 0, 1)
            };
            return true;
        }
    }
}
