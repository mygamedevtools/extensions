/**
 * ReadOnlyDrawer.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 6/5/2018 (en-US)
 * Reference from It3ration: https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
 */

using UnityEditor;
using UnityEngine;

namespace UnityTools.Attributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, true);

        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            bool wasEnabled = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(rect, prop, label, true);
            GUI.enabled = wasEnabled;
        }
    }
}