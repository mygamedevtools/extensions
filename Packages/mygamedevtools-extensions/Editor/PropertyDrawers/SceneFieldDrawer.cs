/**
 * SceneFieldDrawer.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 6/11/2020 (en-US)
 */

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameDevTools.Attributes
{
    [CustomPropertyDrawer(typeof(SceneFieldAttribute))]
    public class SceneFieldDrawer : PropertyDrawer
    {
        SceneAsset scene;
        bool invalid;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (invalid)
                return EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2;
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            invalid = false;
            EditorGUI.BeginProperty(position, label, property);
            position.height = EditorGUIUtility.singleLineHeight;
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                bool enabledValue = GUI.enabled;
                GUI.enabled = false;
                EditorGUI.TextField(position, label, "Not supported. Int required");
                GUI.enabled = enabledValue;
                return;
            }
            if (property.intValue >= 0 && property.intValue < EditorBuildSettings.scenes.Length)
                scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(SceneUtility.GetScenePathByBuildIndex(property.intValue));
            scene = (SceneAsset)EditorGUI.ObjectField(position, label, scene, typeof(SceneAsset), false);
            if (scene != null)
            {
                var scenePath = AssetDatabase.GetAssetPath(scene);
                var buildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
                if (buildIndex >= 0)
                    property.intValue = buildIndex;
                else
                {
                    invalid = true;
                    property.intValue = -1;
                    position.height = EditorGUIUtility.singleLineHeight;
                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                    position = EditorGUI.IndentedRect(position);
                    EditorGUI.HelpBox(position, "This scene is not active in the build settings and will lost its reference.", MessageType.Error);
                    position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                    position.width /= 2;
                    if (GUI.Button(position, "Fix Build Settings"))
                    {
                        var currentBuildScenes = EditorBuildSettings.scenes;
                        var sceneGuid = new GUID(AssetDatabase.AssetPathToGUID(scenePath));
                        var buildScene = currentBuildScenes.FirstOrDefault(s => s.guid == sceneGuid);
                        var newBuildScenes = currentBuildScenes.ToList();
                        if (buildScene != null)
                            newBuildScenes.Find(s => s.guid == sceneGuid).enabled = true;
                        else
                            newBuildScenes.Add(new EditorBuildSettingsScene(sceneGuid, true));
                        EditorBuildSettings.scenes = newBuildScenes.ToArray();
                        invalid = false;
                    }
                    position.x += position.width;
                    if (GUI.Button(position, "Reset Field"))
                    {
                        scene = null;
                        invalid = false;
                    }
                }
            }
            else
                property.intValue = -1;
            EditorGUI.EndProperty();
        }
    }
}