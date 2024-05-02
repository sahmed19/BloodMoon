using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BloodMoon.Utils
{
    public static class TransformUtils
    {

        public static void SetHeight(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

    }
}
