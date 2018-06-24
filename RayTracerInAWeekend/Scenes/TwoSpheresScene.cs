using System.Numerics;
using System.Windows.Media;
using RayTracerInAWeekend.Hitables;
using RayTracerInAWeekend.Materials;
using RayTracerInAWeekend.Textures;

namespace RayTracerInAWeekend.Scenes
{
    class TwoSpheresScene : IScene
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
            return new HitableList
            {
                new Sphere(new Vector3(0, -10, 0), 10, new Lambertian(new CheckerTexture(new ConstantTexture(Colors.GhostWhite), new ConstantTexture(Colors.ForestGreen)))),
                new Sphere(new Vector3(0, 10, 0), 10, new Lambertian(new CheckerTexture(new ConstantTexture(Colors.GhostWhite), new ConstantTexture(Colors.DeepPink))))
            };
        }
    }
}
