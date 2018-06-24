
using System;
using System.Numerics;

namespace RayTracerInAWeekend.Textures
{
    public struct PerlinNoise
    {
        public Vector3[] RanVec;
        public int[] PermX, PermY, PermZ;

        public float GetNoise(Vector3 hitPoint)
        {
            float u = (float) (hitPoint.X - Math.Floor(hitPoint.X));
            u = u * u * (3 - 2 * u);
            float v = (float) (hitPoint.Y - Math.Floor(hitPoint.Y));
            v = v * v * (3 - 2 * v);
            float w = (float) (hitPoint.Z - Math.Floor(hitPoint.Z));
            w = w * w * (3 - 2 * w);
            int i = (int) Math.Floor(hitPoint.X);
            int j = (int) Math.Floor(hitPoint.Y);
            int k = (int) Math.Floor(hitPoint.Z);
            Vector3[,,] c = new Vector3[2, 2, 2];
            for (int di = 0; di < 2; di++)
            {
                for (int dj = 0; dj < 2; dj++)
                {
                    for (int dk = 0; dk < 2; dk++)
                    {
                        c[di, dj, dk] = RanVec[PermX[(i + di) & 255] ^ PermY[(j + dj) & 255] ^ PermZ[(k + dk) & 255]];
                    }
                }
            }
            return TrilinearInterpolate(c, u, v, w);
        }

        public float GetTurbulentNoise(Vector3 hitPoint, int depth = 7)
        {
            float accum = 0;
            Vector3 temp = hitPoint;
            float weight = 1.0f;
            for (int i = 0; i < depth; i++)
            {
                accum += weight * GetNoise(temp);
                weight *= 0.5f;
                temp *= 2;
            }
            return Math.Abs(accum);
        }

        private static float TrilinearInterpolate(Vector3[,,] c, float u, float v, float w)
        {
            float uu = u * u * (3 - 2 * u);
            float vv = v * v * (3 - 2 * v);
            float ww = w * w * (3 - 2 * w);
            float accum = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Vector3 weight = new Vector3(u - i, v - j, w - k);
                        accum +=
                            (i * uu + (1 - i) * (1 - uu)) *
                            (j * vv + (1 - j) * (1 - vv)) *
                            (k * ww + (1 - k) * (1 - ww)) *
                            Vector3.Dot(c[i, j, k], weight);

                    }
                }
            }
            return accum;
        }
    }
    static class PerlinNoiseGenerator
    {
        public static PerlinNoise GeneratePerlinNoise() => new PerlinNoise()
        {
            RanVec = PerlinGenerate(),
            PermX = PerlinGeneratePerm(),
            PermY = PerlinGeneratePerm(),
            PermZ = PerlinGeneratePerm()
        };

        private static Vector3[] PerlinGenerate()
        {
            Vector3[] res = new Vector3[256];
            for (int i = 0; i < 256; i++)
            {
                res[i] = new Vector3(2 * VectorHelpers.RandomFloat() - 1, 2 * VectorHelpers.RandomFloat() - 1, 2 * VectorHelpers.RandomFloat() - 1).GetUnitVector();
            }
            return res;
        }

        private static int[] PerlinGeneratePerm()
        {
            int[] res = new int[256];
            for (int i = 0; i < 256; i++)
            {
                res[i] = i;
            }
            // Permute the array of 0..255
            for (int i = 0; i < 256; i++)
            {
                int swithedIndex = (int) ((i + 1) * VectorHelpers.RandomFloat());
                int temp = res[i];
                res[i] = res[swithedIndex];
                res[swithedIndex] = temp;
            }
            return res;
        }
    }
}
