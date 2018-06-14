
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
        private const double T_MIN = 0.01;
        private const int AA_POINTS = 4;
        private const int MAX_RECURSION_DEPTH = 50;

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
            

            World = new HitableList
            {
                new Sphere(new Vector3(0, -1000, 0), 1000f, new Lambertian(new Vector3(0.5f, 0.5f, 0.5f)))
            };
            
            Func<float> randFloat = VectorHelpers.RandomFloat;
            for (int i = -5; i < 5; i++)
            {
                for (int j = -5; j < 5; j++)
                {
                    Vector3 center = new Vector3(i + 0.9f * randFloat(), 0.2f, j + 0.9f * randFloat());

                    float materialChoice = randFloat();
                    IMaterial m;
                    if (materialChoice < 0.75)
                    {
                        m = new Lambertian(new Vector3(randFloat() * randFloat(), randFloat() * randFloat(), randFloat() * randFloat()));
                    }
                    else if (materialChoice < 0.95)
                    {
                        m = new Metal(new Vector3(0.5f * (1 + randFloat()), 0.5f * (1 + randFloat()), 0.5f * (1 + randFloat())), 0.5f * randFloat());
                    }
                    else
                    {
                        m = new Dielectric(1.5f);
                    }
                    var s = new Sphere(center, 0.2f, m);
                    World.Add(s);
                }
            }

            World.Add(new Sphere(new Vector3(0, 1, 0), 1.0f, new Dielectric(1.5f)));
            World.Add(new Sphere(new Vector3(-4, 1, 0), 1.0f, new Lambertian(new Vector3(0.4f, 0.2f, 0.1f))));
            World.Add(new Sphere(new Vector3(4, 1, 0), 1.0f, new Metal(new Vector3(0.7f, 0.6f, 0.5f), 0)));

            Vector3 lookFrom = new Vector3(8f, 3f, 2.5f);
            Vector3 lookAt = new Vector3(0, 0.5f, 0);
            Vector3 vup = Vector3.UnitY;
            float vFov = 20;
            float aspect = (1.0f * imgW) / imgH;
            float aperture = 0.05f;
            float focalDistance = (lookFrom - lookAt).Length();
            Camera = new Camera(lookFrom, lookAt, vup, vFov, aspect, aperture, focalDistance);

            float invWidth = 1.0f / imgW;
            float invHeight = 1.0f / imgH;
            Parallel.For(0, imgH, y =>
            {
                for (int x = 0; x < imgW; x++)
                {
                    Vector4 color = Vector4.Zero;
                    for (uint sample = 0; sample < AA_POINTS; sample++)
                    {
                        float xOffset = (x + randFloat() - 0.5f) * invWidth;
                        float yOffset = (y + randFloat() - 0.5f) * invHeight;

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
