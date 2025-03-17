#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace BloodMoon.Editor
{
    public static class GizmoExtension
    {
        public static void DrawArrow(Vector3 from, Vector3 to, float arrowHeadLength = 0.3f)
        {
            Gizmos.DrawLine(from, to);
            Vector3 cameraPos = SceneView.currentDrawingSceneView.camera.transform.position;
            Vector3 toToCamPos = (cameraPos - to).normalized;
            Vector3 displacement = (to - from).normalized;

            Vector3 cross = Vector3.Cross(displacement, toToCamPos);
            Vector3 leftArrowhead = Vector3.Lerp(displacement, cross, 0.5f).normalized * arrowHeadLength;
            Vector3 rightArrowhead = Vector3.Lerp(displacement, -cross, 0.5f).normalized * arrowHeadLength;
            Gizmos.DrawLine(to, to - leftArrowhead);
            Gizmos.DrawLine(to, to - rightArrowhead);
        }
    }
}
#endif