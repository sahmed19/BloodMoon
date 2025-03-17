using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

namespace BloodMoon.Utils
{
    public static class MonoUtils
    {
        public static void DestroyAllGameObjectsOfType<T>() where T : Component
        {
            foreach(var obj in GameObject.FindObjectsOfType<T>())
            {
                GameObject.Destroy(obj.gameObject);
            }
        }

        public static IEnumerator WaitUntil(Func<bool> predicate)
        {
            if(!predicate.Invoke())
                yield return null;
        }

        public static void StopAndStartCoroutine(this MonoBehaviour mono, IEnumerator instance, IEnumerator coroutine)
        {
            if (instance != null)
                mono.StopCoroutine(instance);

            instance = coroutine;
            mono.StartCoroutine(instance);
        }

        public static IEnumerator LoopOverDuration(float duration, Action<float> response)
        {
            for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
            {
                response.Invoke(t);
                yield return null;
            }
            response.Invoke(1.0f);
        }

        public static int SiblingIndexComparison<T>(T c1, T c2) where T : Component
        {
            var t1 = c1.transform;
            var t2 = c2.transform;

            if (t1 && !t2) // c2 is null but c1 isn't, consider c1 greater
                return 1;
            if (!t1 && t2) // c1 is null but c2 isn't, consider c2 greater
                return -1;
            if (t1.parent != t2.parent) // if parents aren't the same move up one layer
                return SiblingIndexComparison(t1.parent, t2.parent);

            return t1.GetSiblingIndex().CompareTo(t2.GetSiblingIndex()); // if same parent, compare sibling index
        }
    }
}