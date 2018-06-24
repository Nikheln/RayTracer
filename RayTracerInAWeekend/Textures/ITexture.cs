using System.Numerics;

namespace RayTracerInAWeekend.Textures
{
    public interface ITexture
    {
        /// <summary>
        /// Get the RGB color represented by a <see cref="Vector3"/> in the specific coordinates of this texture.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        Vector3 GetValue(float u, float v, Vector3 hitPoint);
    }
}
