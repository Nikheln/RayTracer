using System;
using System.Numerics;
using RayTracerInAWeekend.Materials;

namespace RayTracerInAWeekend.Scenes
{
    class BookScene : IScene
    {
        public HitableList GetSceneWorld()
        {
            var world = new HitableList
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
                    world.Add(s);
                }
            }

            world.Add(new Sphere(new Vector3(0, 1, 0), 1.0f, new Dielectric(1.5f)));
            world.Add(new Sphere(new Vector3(-4, 1, 0), 1.0f, new Lambertian(new Vector3(0.4f, 0.2f, 0.1f))));
            world.Add(new Sphere(new Vector3(4, 1, 0), 1.0f, new Metal(new Vector3(0.7f, 0.6f, 0.5f), 0)));

            return world;
        }

        public Camera GetDefaultCamera(float aspectRatio)
        {
            Vector3 lookFrom = new Vector3(9.5f, 2f, 2.5f);
            Vector3 lookAt = new Vector3(3, 0.5f, 0.65f);
            Vector3 vup = Vector3.UnitY;
            float vFov = 25;
            float aperture = 0.1f;
            float focalDistance = (lookFrom - lookAt).Length();

            return new Camera(lookFrom, lookAt, vup, vFov, aspectRatio, aperture, focalDistance);
        }
    }
}
