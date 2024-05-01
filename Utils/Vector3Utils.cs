using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSwallowFalls
{
    public static class Vector3Utils
    {
        public static Vector2 XZ(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }
    }
}
