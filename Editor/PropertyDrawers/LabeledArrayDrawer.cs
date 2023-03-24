/**
 * LabeledArrayDrawer.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 12/28/2017 (en-US)
 * Reference from John Avery: https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
 */

using UnityEngine;
using UnityEditor;
using System.Linq;

namespace MyGameDevTools.Attributes
{
    [CustomPropertyDrawer(typeof(LabeledArrayAttribute))]
    public class LabeledArrayDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);
            try
            {
                var path = property.propertyPath;
                int pos = int.Parse(path.Split('[').LastOrDefault().TrimEnd(']'));
                EditorGUI.PropertyField(rect, property, new GUIContent(ObjectNames.NicifyVariableName(((LabeledArrayAttribute)attribute).names[pos])), true);
            }
            catch
            {
                EditorGUI.PropertyField(rect, property, label, true);
            }
            EditorGUI.EndProperty();
        }
    }
}
