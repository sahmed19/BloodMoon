using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodMoon.Utils
{
    public static class Vector3Utils
    {
        public static Vector3 Append(this Vector2 v, float f) => new Vector3(v.x, v.y, f);
        public static Vector2 XY(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
        public static Vector2 XZ(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }
        public static Vector2 Interpolate(this Rect rect, float x, float y)
        {
            return new Vector2(
                Mathf.Lerp(rect.min.x, rect.max.x, x),
                Mathf.Lerp(rect.min.y, rect.max.y, y));
        }
        public static float RoundTo(this float f, float round)
        {
            return Mathf.Round(f / round) * round;
        }
    }
}