
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using RayTracerInAWeekend.Scenes;

namespace RayTracerInAWeekend
{
    static class Renderer
    {
        public const int IMG_WIDTH = 1200;
        public const int IMG_HEIGHT = 800;
        private const float T_MAX = 100f;
        private const float T_MIN = 0.01f;
        private const int AA_POINTS = 10;
        private const int MAX_RECURSION_DEPTH = 100;

        private static int bpp;

        private static HitableList World;
        private static Camera Camera;

        public static void Render(int _bpp, byte[] buffer)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            
            bpp = _bpp;
            
            IScene scene = new BookScene();
            World = scene.GetSceneWorld();
            Camera = scene.GetDefaultCamera((1f * IMG_WIDTH) / IMG_HEIGHT);
            
            Parallel.For(0, IMG_HEIGHT, y =>
            {
                for (int x = 0; x < IMG_WIDTH; x++)
                {
                    Vector4 color = Vector4.Zero;
                    for (uint sample = 0; sample < AA_POINTS; sample++)
                    {
                        double xOffset = (x + VectorHelpers.RandomFloat() - 0.5f) / ((double) IMG_WIDTH);
                        double yOffset = (y + VectorHelpers.RandomFloat() - 0.5f) / ((double) IMG_HEIGHT);

                        Ray r = Camera.GetRay(xOffset, yOffset);
                        color += GetColorFor(r, 0);
                    }
                    color /= AA_POINTS;

                    int offset = CoordsToBufferIndex(x, y);
                    buffer[offset++] = DoubleToByte(color.X);
                    buffer[offset++] = DoubleToByte(color.Y);
                    buffer[offset++] = DoubleToByte(color.Z);
                }
            });

            st.Stop();
            Console.WriteLine("Scene rendered in " + st.ElapsedMilliseconds + " ms.");
        }

        private static Vector4 GetColorFor(Ray r, int recursionDepth)
        {
            if (World.Hit(r, T_MIN, T_MAX, out HitRecord record))
            {
                if (recursionDepth < MAX_RECURSION_DEPTH && record.Material.Scatter(r, record, out Vector3 attenuation, out Ray scattered))
                {
                    return new Vector4(attenuation, 1f) * GetColorFor(scattered, recursionDepth + 1);
                }
                else
                {
                    return Vector4.Zero;
                }
            }
            else
            {
                Vector3 unitVec = r.Direction.GetUnitVector();
                float t = 0.5f * (unitVec.Y + 1f);
                return (1f - t) * Vector4.One + t * new Vector4(0.5f, 0.7f, 1f, 1f);
            }
        }

        private static int CoordsToBufferIndex(int x, int y)
        {
            return ((IMG_HEIGHT - 1 - y) * IMG_WIDTH + x) * bpp;
        }

        /// <summary>
        /// Convert a double in range 0...1 to an unsigned byte (range 0...255)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static byte DoubleToByte(double input)
        {
            return (byte) (255.0 * input);
        }
    }
}
