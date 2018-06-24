using System;
using System.Numerics;
using System.Windows.Media;

namespace RayTracerInAWeekend.Textures
{
    class ConstantTexture : ITexture
    {
        private readonly Vector3 ColorVec;

        public ConstantTexture(Color color)
        {
            ColorVec = new Vector3(color.R / (float) byte.MaxValue, color.G / (float) byte.MaxValue, color.B / (float) byte.MaxValue);
        }

        public ConstantTexture(Vector3 color)
        {
            ColorVec = color;
        }

        public Vector3 GetValue(float u, float v, Vector3 hitPoint)
        {
            return ColorVec;
        }
    }
}
