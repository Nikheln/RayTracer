using System;
using System.Numerics;

namespace RayTracerInAWeekend.Textures
{
    public class NoiseTexture : ITexture
    {
        private readonly PerlinNoise Noise;
        private readonly float Scale;

        public NoiseTexture() : this(1.0f)
        { }

        public NoiseTexture(float scale)
        {
            Scale = scale;
            Noise = PerlinNoiseGenerator.GeneratePerlinNoise();
        }

        public Vector3 GetValue(float u, float v, Vector3 hitPoint)
        {
            //return Vector3.One * Noise.GetTurbulentNoise(hitPoint);
            return Vector3.One * 0.5f * (float) (1 + Math.Sin(Scale * hitPoint.Z + 10f * Noise.GetTurbulentNoise(hitPoint)));
        }
    }
}
