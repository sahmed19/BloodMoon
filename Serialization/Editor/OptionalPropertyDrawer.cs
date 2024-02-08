using UnityEditor;
using UnityEngine;

namespace BloodMoon.Serialization.Editor
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("Value");
            var enabledProperty = property.FindPropertyRelative("Enabled");

            position.width -= 24;
            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            {
                EditorGUI.PropertyField(position, valueProperty, label, true);
            }
            EditorGUI.EndDisabledGroup();
            
            position.x += position.width + 24;
            position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
            position.x -= position.width;
            EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
        }
    }
}