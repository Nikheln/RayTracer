using System;

namespace RayTracerInAWeekend
{
    public class Vec3
    {
        // Position
        public double x, y, z;
        // Color
        public double r => x;
        public double g => y;
        public double b => z;

        public static readonly Vec3 Zero = new Vec3(0, 0, 0);
        public static readonly Vec3 One = new Vec3(1, 1, 1);

        public Vec3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vec3 operator + (Vec3 v1, Vec3 v2)
        {
            return new Vec3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        public static Vec3 operator - (Vec3 v1, Vec3 v2)
        {
            return new Vec3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
        public static Vec3 operator * (double d, Vec3 v)
        {
            return new Vec3(v.x * d, v.y * d, v.z * d);
        }

        public static Vec3 operator * (Vec3 v, double d)
        {
            return d * v;
        }

        public static Vec3 operator * (Vec3 v1, Vec3 v2)
        {
            return new Vec3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }

        public static Vec3 operator / (Vec3 v, double d)
        {
            return v * (1.0 / d);
        }


        public static double DotProduct(Vec3 v1, Vec3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public Vec3 GetUnitVector()
        {
            double k = Length;
            return new Vec3(x / k, y / k, z / k);
        }

        public double Length => Math.Sqrt(x * x + y * y + z * z);

        public double SquaredLength => x * x + y * y + z * z;


        public static Vec3 GetRandomInUnitSphere()
        {
            Random rng = new Random();
            Vec3 p;
            do
            {
                p = 2.0 * new Vec3(rng.NextDouble(), rng.NextDouble(), rng.NextDouble()) - One;
            } while (p.SquaredLength >= 1.0);

            return p;
        }
    }
}
