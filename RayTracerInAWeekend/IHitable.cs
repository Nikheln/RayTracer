using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using RayTracerInAWeekend.Materials;

namespace RayTracerInAWeekend
{
    public struct HitRecord
    {
        public float t;
        public Vector3 HitPoint;
        public Vector3 SurfaceNormal;
        public IMaterial Material;
    }

    interface IHitable
    {
        IMaterial Material { get; }
        bool IsHitBy(Ray r, float tMin, float tMax, out HitRecord record);
    }

    class HitableList : List<IHitable>
    {
        public bool Hit(Ray r, float tMin, float tMax, out HitRecord record)
        {
            bool hitAnything = false;
            HitRecord closestRecord = new HitRecord() { t = tMax };
            foreach (var hitable in this)
            {
                if (hitable.IsHitBy(r, tMin, closestRecord.t, out HitRecord tempRecord))
                {
                    hitAnything = true;
                    closestRecord = tempRecord;
                }
            }

            record = closestRecord;
            return hitAnything;
        }
    }
}
