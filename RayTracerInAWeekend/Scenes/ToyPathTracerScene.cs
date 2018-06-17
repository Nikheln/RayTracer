using System.Numerics;
using RayTracerInAWeekend.Materials;

namespace RayTracerInAWeekend.Scenes
{
    class ToyPathTracerScene : IScene
    {
        public Camera GetDefaultCamera(float aspectRatio)
        {
            Vector3 lookFrom = new Vector3(0, 2, 3);
            Vector3 lookAt = Vector3.Zero;
            Vector3 vup = Vector3.UnitY;
            float vFov = 60f;
            float aperture = 0.02f;
            float focalDistance = 3;

            return new Camera(lookFrom, lookAt, vup, vFov, aspectRatio, aperture, focalDistance);
        }

        public HitableList GetSceneWorld()
        {
            var _materials = new IMaterial[]
            {
                new Lambertian(new Vector3(0.8f, 0.8f, 0.8f)),
                new Lambertian(new Vector3(0.8f, 0.4f, 0.4f)),
                new Lambertian(new Vector3(0.4f, 0.8f, 0.4f)),
                new Metal(new Vector3(0.4f, 0.4f, 0.8f), 0),
                new Metal(new Vector3(0.4f, 0.8f, 0.4f), 0),
                new Metal(new Vector3(0.4f, 0.8f, 0.4f), 0.2f),
                new Metal(new Vector3(0.4f, 0.8f, 0.4f), 0.6f),
                new Dielectric(1.5f),
                new Lambertian(new Vector3(0.8f, 0.6f, 0.2f)),
                new Lambertian(new Vector3(0.1f, 0.1f, 0.1f)),
                new Lambertian(new Vector3(0.2f, 0.2f, 0.2f)),
                new Lambertian(new Vector3(0.3f, 0.3f, 0.3f)),
                new Lambertian(new Vector3(0.4f, 0.4f, 0.4f)),
                new Lambertian(new Vector3(0.5f, 0.5f, 0.5f)),
                new Lambertian(new Vector3(0.6f, 0.6f, 0.6f)),
                new Lambertian(new Vector3(0.7f, 0.7f, 0.7f)),
                new Lambertian(new Vector3(0.8f, 0.8f, 0.8f)),
                new Lambertian(new Vector3(0.9f, 0.9f, 0.9f)),
                new Metal(new Vector3(0.1f, 0.1f, 0.1f), 0f),
                new Metal(new Vector3(0.2f, 0.2f, 0.2f), 0f),
                new Metal(new Vector3(0.3f, 0.3f, 0.3f), 0f),
                new Metal(new Vector3(0.4f, 0.4f, 0.4f), 0f),
                new Metal(new Vector3(0.5f, 0.5f, 0.5f), 0f),
                new Metal(new Vector3(0.6f, 0.6f, 0.6f), 0f),
                new Metal(new Vector3(0.7f, 0.7f, 0.7f), 0f),
                new Metal(new Vector3(0.8f, 0.8f, 0.8f), 0f),
                new Metal(new Vector3(0.9f, 0.9f, 0.9f), 0f),
                new Metal(new Vector3(0.8f, 0.1f, 0.1f), 0f),
                new Metal(new Vector3(0.8f, 0.5f, 0.1f), 0f),
                new Metal(new Vector3(0.8f, 0.8f, 0.1f), 0f),
                new Metal(new Vector3(0.4f, 0.8f, 0.1f), 0f),
                new Metal(new Vector3(0.1f, 0.8f, 0.1f), 0f),
                new Metal(new Vector3(0.1f, 0.8f, 0.5f), 0f),
                new Metal(new Vector3(0.1f, 0.8f, 0.8f), 0f),
                new Metal(new Vector3(0.1f, 0.1f, 0.8f), 0f),
                new Metal(new Vector3(0.5f, 0.1f, 0.8f), 0f),
                new Lambertian(new Vector3(0.8f, 0.1f, 0.1f)),
                new Lambertian(new Vector3(0.8f, 0.5f, 0.1f)),
                new Lambertian(new Vector3(0.8f, 0.8f, 0.1f)),
                new Lambertian(new Vector3(0.4f, 0.8f, 0.1f)),
                new Lambertian(new Vector3(0.1f, 0.8f, 0.1f)),
                new Lambertian(new Vector3(0.1f, 0.8f, 0.5f)),
                new Lambertian(new Vector3(0.1f, 0.8f, 0.8f)),
                new Lambertian(new Vector3(0.1f, 0.1f, 0.8f)),
                new Metal(new Vector3(0.5f, 0.1f, 0.8f), 0f),
                new Lambertian(new Vector3(0.1f, 0.2f, 0.5f))
            };
            int i = 0;

            return new HitableList()
            {
                new Sphere(new Vector3(0,-100.5f,-1), 100, _materials[i++]),
                new Sphere(new Vector3(2,0,-1), 0.5f, _materials[i++]),
                new Sphere(new Vector3(0,0,-1), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-2,0,-1), 0.5f, _materials[i++]),
                new Sphere(new Vector3(2,0,1), 0.5f, _materials[i++]),
                new Sphere(new Vector3(0,0,1), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-2,0,1), 0.5f, _materials[i++]),
                new Sphere(new Vector3(0.5f,1,0.5f), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-1.5f,1.5f,0f), 0.3f, _materials[i++]),
                new Sphere(new Vector3(4,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(3,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(2,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(1,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(0,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-1,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-2,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-3,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-4,0,-3), 0.5f, _materials[i++]),
                new Sphere(new Vector3(4,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(3,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(2,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(1,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(0,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-1,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-2,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-3,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-4,0,-4), 0.5f, _materials[i++]),
                new Sphere(new Vector3(4,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(3,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(2,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(1,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(0,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-1,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-2,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-3,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-4,0,-5), 0.5f, _materials[i++]),
                new Sphere(new Vector3(4,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(3,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(2,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(1,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(0,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-1,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-2,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-3,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(-4,0,-6), 0.5f, _materials[i++]),
                new Sphere(new Vector3(1.5f,1.5f,-2), 0.3f, _materials[i++]),
            };
        }
    }
}
