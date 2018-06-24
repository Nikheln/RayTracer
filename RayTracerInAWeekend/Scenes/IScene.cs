
using RayTracerInAWeekend.Hitables;

namespace RayTracerInAWeekend.Scenes
{
    interface IScene
    {
        HitableList GetSceneWorld();

        Camera GetDefaultCamera(float aspectRatio);
    }
}
