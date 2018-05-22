
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using RayTracerInAWeekend.Materials;

namespace RayTracerInAWeekend
{
    static class Renderer
    {
        private const double T_MAX = 100;
        private const double T_MIN = 0.001;
        private const int AA_POINTS = 16;
        private const int MAX_RECURSION_DEPTH = 50;

        private static Random Rng;

        private static int imgW, imgH, bpp;

        private static HitableList World;
        private static Camera Camera;

        public static void Render(int _imgW, int _imgH, int _bpp, byte[] buffer)
        {
            Stopwatch st = new Stopwatch();
            st.Start();

            imgW = _imgW;
            imgH = _imgH;
            bpp = _bpp;


            Rng = new Random();

            World = new HitableList();

            Random rng = new Random("tama".GetHashCode());
            for (int i = 0; i < 100; i++)
            {
                double r = rng.NextDouble();
                IMaterial m;
                Vector3 a = new Vector3((float) rng.NextDouble(), (float) rng.NextDouble(), (float) rng.NextDouble());
                if (r < 0.33)
                {
                    m = new Metal(a, (float) rng.NextDouble() * 0.5f);
                }
                else if (r < 0.66)
                {
                    m = new Dielectric((float) rng.NextDouble() * 2, a);
                }
                else
                {
                    m = new Lambertian(a);
                }
                var s = new Sphere(new Vector3((float)rng.NextDouble() * 10, (float)rng.NextDouble() * 5, (float)rng.NextDouble() * (-10)), (float)rng.NextDouble() * 2, m);
                World.Add(s);
            }
            Camera = new Camera(new Vector3(0,2,1), new Vector3(0,0,-1), new Vector3(0,1,0), 60, (1.0f*imgW)/imgH);

            Parallel.For(0, imgH, y =>
            {
                for (int x = 0; x < imgW; x++)
                {
                    Vector3 color = new Vector3(0, 0, 0);
                    for (int _ = 0; _ < AA_POINTS; _++)
                    {
                        float xOffset = (float) (x + 0.1 * (Rng.NextDouble() - 0.5)) / (1.0f * imgW);
                        float yOffset = (float) (y + 0.1 * (Rng.NextDouble() - 0.5)) / (1.0f * imgH);
                        Vector3 colorSample = GetColorFor(Camera.GetRay(xOffset, yOffset), 0);
                        color += colorSample;
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

        private static Vector3 GetColorFor(Ray r, int recursionDepth)
        {
            if (World.Hit(r, T_MIN, T_MAX, out HitRecord record))
            {
                if (recursionDepth < MAX_RECURSION_DEPTH && record.Material.Scatter(r, record, out Vector3 attenuation, out Ray scattered))
                {
                    return attenuation * GetColorFor(scattered, recursionDepth + 1);
                }
                else
                {
                    return Vector3.Zero;
                }
            }
            else
            {
                Vector3 unitVec = r.Direction / r.Direction.Length();
                double t = 0.5 * (unitVec.Y + 1.0);
                return ((float) (1.0 - t) * Vector3.One + (float) t * new Vector3(0.5f, 0.7f, 1.0f));
            }
        }

        private static int CoordsToBufferIndex(int x, int y)
        {
            return ((imgH - 1 - y) * imgW + x) * bpp;
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
