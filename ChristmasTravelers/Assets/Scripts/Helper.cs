
using UnityEngine;

public static class Helper
{
    /// <summary>
    /// Rotate a vector with a certain angle 
    /// </summary>
    /// <param name="v">The vector to rotate</param>
    /// <param name="delta">The anle in radians</param>
    /// <returns>The rotated vector</returns>
    public static Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    public static float Gaussian(float sigma, float mu, float x)
    {
        return (1 / (sigma * Mathf.Sqrt(2*Mathf.PI))) * Mathf.Exp( -Mathf.Pow(x-mu,2) / (2 * Mathf.Pow(sigma,2)));
    }
}