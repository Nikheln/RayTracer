using System;
using System.Numerics;

namespace RayTracerInAWeekend.Textures
{
    class CheckerTexture : ITexture
    {
        private readonly ITexture EvenTexture, OddTexture;

        public CheckerTexture(ITexture texture1, ITexture texture2)
        {
            EvenTexture = texture1;
            OddTexture = texture2;
        }

        public Vector3 GetValue(float u, float v, Vector3 hitPoint)
        {
            double sineFunc(float val) => Math.Sin(10 * val);
            double sines = sineFunc(hitPoint.X) * sineFunc(hitPoint.Y) * sineFunc(hitPoint.Z);
            if (sines < 0)
            {
                return OddTexture.GetValue(u, v, hitPoint);
            }
            else
            {
                return EvenTexture.GetValue(u, v, hitPoint);
            }
        }
    }
}
