
using System;
using System.Numerics;

namespace RayTracerInAWeekend
{
    class Camera
    {
        private Vector3 LowerLeftCorner, Horizontal, Vertical, Origin;

        public Camera(Vector3 lookFrom, Vector3 lookAt, Vector3 vup, float vFov, float aspect)
        {
            float vTheta = vFov * (float) (Math.PI / 180);
            float halfHeight = (float) Math.Tan(vTheta / 2);
            float halfWidth = aspect * halfHeight;

            Origin = lookFrom;

            Vector3 w = (lookFrom - lookAt).GetUnitVector();
            Vector3 u = Vector3.Cross(vup, w).GetUnitVector();
            Vector3 v = Vector3.Cross(w, u).GetUnitVector();

            LowerLeftCorner = lookFrom - halfWidth * u - halfHeight * v - w;
            Horizontal = 2 * halfWidth * u;
            Vertical = 2 * halfHeight * v;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
        }
    }
}
