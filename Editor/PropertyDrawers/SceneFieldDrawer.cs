/**
 * SceneFieldDrawer.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 6/11/2020 (en-US)
 */

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyUnityTools.Attributes
{
    [CustomPropertyDrawer(typeof(SceneFieldAttribute))]
    public class SceneFieldDrawer : PropertyDrawer
    {
        SceneAsset scene;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUI.GetPropertyHeight(property, label);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                bool enabledValue = GUI.enabled;
                GUI.enabled = false;
                EditorGUI.TextField(position, label, "Not supported");
                GUI.enabled = enabledValue;
                return;
            }
            if (property.intValue >= 0 && property.intValue < EditorBuildSettings.scenes.Length)
                scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(SceneUtility.GetScenePathByBuildIndex(property.intValue));
            scene = (SceneAsset)EditorGUI.ObjectField(position, label, scene, typeof(SceneAsset), false);
            if (scene != null)
                property.intValue = SceneUtility.GetBuildIndexByScenePath(AssetDatabase.GetAssetPath(scene));
            EditorGUI.EndProperty();
        }
    }
}