using System.Numerics;
using RayTracerInAWeekend.Hitables;
using RayTracerInAWeekend.Materials;
using RayTracerInAWeekend.Textures;

namespace RayTracerInAWeekend.Scenes
{
    class PerlinNoiseScene : IScene
    {
        public Camera GetDefaultCamera(float aspectRatio)
        {
            Vector3 lookFrom = new Vector3(13, 2, 3);
            Vector3 lookAt = Vector3.Zero;
            Vector3 vup = Vector3.UnitY;
            float vFov = 25f;
            float aperture = 0;
            float focalDistance = 10;

            return new Camera(lookFrom, lookAt, vup, vFov, aspectRatio, aperture, focalDistance);
        }

        public HitableList GetSceneWorld()
        {
            var perlinTexture = new NoiseTexture(1f);
            return new HitableList()
            {
                new Sphere(new Vector3(0, -1000, 0), 1000, new Lambertian(perlinTexture)),
                new Sphere(new Vector3(0, 2, 0), 2, new Lambertian(perlinTexture))
            };
        }
    }
}
