
using System;
using System.Numerics;
using RayTracerInAWeekend.Materials;

namespace RayTracerInAWeekend
{
    class Sphere : IHitable
    {
        public Vector3 Center;
        public float Radius;

        public Sphere(Vector3 center, float radius, IMaterial material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }
        public IMaterial Material { get; }

        public bool IsHitBy(Ray r, double tMin, double tMax, out HitRecord record)
        {
            Vector3 oc = r.Origin - Center;
            float a = Vector3.Dot(r.Direction, r.Direction);
            float b = Vector3.Dot(oc, r.Direction);
            float c = Vector3.Dot(oc, oc) - Radius * Radius;

            double discriminant = b * b - a * c;

            if (discriminant > 0)
            {
                float discrSqrt = (float) Math.Sqrt(discriminant);
                float _t = (-b - discrSqrt) / a;
                if (_t < tMax && _t > tMin)
                {
                    Vector3 hitPoint = r.PointAtParameter(_t);
                    record = new HitRecord()
                    {
                        t = _t,
                        HitPoint = hitPoint,
                        SurfaceNormal = (hitPoint - Center) / Radius,
                        Material = Material
                    };
                    return true;
                }

                _t = (-b + discrSqrt) / a;
                if (_t < tMax && _t > tMin)
                {
                    Vector3 hitPoint = r.PointAtParameter(_t);
                    record = new HitRecord()
                    {
                        t = _t,
                        HitPoint = hitPoint,
                        SurfaceNormal = (hitPoint - Center) / Radius,
                        Material = Material
                    };
                    return true;
                }
            }
            record = new HitRecord();
            return false;
        }
    }
}
