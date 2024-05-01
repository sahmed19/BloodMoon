using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSwallowFalls
{
    public static class TransformUtils
    {

        public static void SetHeight(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

    }
}
