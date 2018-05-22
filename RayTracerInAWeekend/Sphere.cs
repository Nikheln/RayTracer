
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
            this.material = material;
        }

        private IMaterial material;
        public IMaterial Material => material;

        public bool IsHitBy(Ray r, double tMin, double tMax, out HitRecord record)
        {
            Vector3 oc = r.Origin - Center;
            double a = Vector3.Dot(r.Direction, r.Direction);
            double b = Vector3.Dot(oc, r.Direction);
            double c = Vector3.Dot(oc, oc) - Radius * Radius;

            double discriminant = b * b - a * c;

            if (discriminant > 0)
            {
                double discrSqrt = Math.Sqrt(discriminant);
                float _t = (float) ((-b - discrSqrt) / a);
                if (_t <= tMin || _t >= tMax)
                {
                    _t = (float) ((-b + discrSqrt) / a);
                }

                if (_t > tMin && _t < tMax)
                {
                    Vector3 hitPoint = r.PointAtParameter(_t);
                    Vector3 normal = (hitPoint - Center) / Radius;
                    record = new HitRecord()
                    {
                        t = _t,
                        HitPoint = hitPoint,
                        SurfaceNormal = normal,
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
