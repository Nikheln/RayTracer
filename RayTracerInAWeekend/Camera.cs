
using System;
using System.Numerics;

namespace RayTracerInAWeekend
{
    class Camera
    {
        private Vector3 LowerLeftCorner, Horizontal, Vertical, Origin, U, V, W;
        private readonly float LensRadius;

        public Camera(Vector3 lookFrom, Vector3 lookAt, Vector3 vup, float vFov, float aspect, float aperture, float focalDistance)
        {
            LensRadius = aperture / 2f;

            float vTheta = vFov * (float) (Math.PI / 180f);
            float halfHeight = (float) Math.Tan(vTheta / 2f);
            float halfWidth = aspect * halfHeight;

            Origin = lookFrom;

            W = (lookFrom - lookAt).GetUnitVector();
            U = Vector3.Cross(vup, W).GetUnitVector();
            V = Vector3.Cross(W, U);

            LowerLeftCorner = Origin - halfWidth * focalDistance * U - halfHeight * focalDistance * V - focalDistance * W;
            Horizontal = 2 * halfWidth * focalDistance * U;
            Vertical = 2 * halfHeight * focalDistance * V;
        }

        public Ray GetRay(double s, double t)
        {
            Vector3 rd = LensRadius * VectorHelpers.GetRandomInUnitDisk();
            Vector3 offset = U * rd.X + V * rd.Y;

            var origin = Origin + offset;
            var direction = LowerLeftCorner + new Vector3((float) (s * Horizontal.X), (float) (s * Horizontal.Y), (float) (s * Horizontal.Z)) + new Vector3((float) (t * Vertical.X), (float) (t * Vertical.Y), (float) (t * Vertical.Z)) - origin;
            return new Ray(origin, direction);
        }
    }
}
