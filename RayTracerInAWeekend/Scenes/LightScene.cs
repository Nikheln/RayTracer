using System.Numerics;
using RayTracerInAWeekend.Hitables;
using RayTracerInAWeekend.Materials;
using RayTracerInAWeekend.Textures;

namespace RayTracerInAWeekend.Scenes
{
    class LightScene : IScene
    {
        public Camera GetDefaultCamera(float aspectRatio)
        {
            Vector3 lookFrom = new Vector3(13, 2, 3);
            Vector3 lookAt = Vector3.Zero;
            Vector3 vup = Vector3.UnitY;
            float vFov = 60f;
            float aperture = 0;
            float focalDistance = 10;

            return new Camera(lookFrom, lookAt, vup, vFov, aspectRatio, aperture, focalDistance);
        }

        public HitableList GetSceneWorld()
        {
            var perlinTexture = new NoiseTexture(1f);
            var lightTexture = new DiffuseLight(new ConstantTexture(new Vector3(4, 4, 4)));
            return new HitableList()
            {
                new Sphere(new Vector3(0, -1000, 0), 1000, new Lambertian(perlinTexture)),
                new Sphere(new Vector3(0, 2, 0), 2, new Lambertian(perlinTexture)),
                new XYRectangle(3, 1, -2, 2, 2, lightTexture),
                new Sphere(new Vector3(0, 8, 0), 3, lightTexture)
            };
        }
    }
}
