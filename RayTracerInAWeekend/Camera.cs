
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

        public Ray GetRay(float s, float t)
        {
            Vector3 rd = LensRadius * VectorHelpers.GetRandomInUnitDisk();
            Vector3 offset = U * rd.X + V * rd.Y;
            return new Ray(Origin + offset, LowerLeftCorner + s * Horizontal + t * Vertical - Origin - offset);
        }
    }
}
