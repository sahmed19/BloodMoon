using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

namespace BloodMoon.Utils
{
    public static class MonoUtils
    {
        public static Vector2 LocalToCanvasSpacePosition(this RectTransform rectTransform) => LocalToCanvasSpacePosition(rectTransform, Vector2.zero);
        public static Vector2 LocalToCanvasSpacePosition(this RectTransform rectTransform, Vector2 localCanvasPoint)
        {
            var worldPos = rectTransform.TransformPoint(localCanvasPoint);
            var canvas = rectTransform.GetComponentInParent<Canvas>();
            var canvasRect = canvas.GetComponent<RectTransform>().rect;
            var camera = canvas.worldCamera;
            var viewportPos = camera.WorldToViewportPoint(worldPos);
            var ret = new Vector2(viewportPos.x * canvasRect.width, viewportPos.y * canvasRect.height);
            return ret;
            
        }

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

        public static IEnumerator TweenOneMinus(this MonoBehaviour mono, Action<float> action, float duration)
        {
            var routine = CR_TweenOneMinus(action, duration);
            mono.StartCoroutine(routine);
            return routine;
        }
        private static IEnumerator CR_TweenOneMinus(Action<float> action, float duration)
        {
            for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
            {
                action(1f-t);
                yield return null;
            }
            action(0f);
        }

        public static IEnumerator Tween(this MonoBehaviour mono, Action<float> action, float duration)
        {
            var routine = CR_Tween(action, duration);
            mono.StartCoroutine(routine);
            return routine;
        }

        private static IEnumerator CR_Tween(Action<float> action, float duration)
        {
            for(float t = 0; t < 1.0f; t += Time.deltaTime/duration)
            {
                action(t);
                yield return null;
            }
            action(1f);
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