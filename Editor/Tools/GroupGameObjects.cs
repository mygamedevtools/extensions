/**
 * GroupGameObjects.cs
 * Created by: Jo�o Borks [joao.borks@gmail.com]
 * Created on: 12/5/2020 (en-US)
 */

using UnityEditor;
using UnityEngine;

namespace MyUnityTools.Extensions
{
    public class GroupGameObjects
    {
        [MenuItem("Tools/Group Selected Objects %g")]
        public static void GroupSelectedObjects()
        {
            var objects = Selection.gameObjects;
            var length = objects.Length;
            if (length <= 1)
                return;

            var parent = new GameObject("Group");
            int i = 0;
            parent.transform.SetParent(objects[i].transform.parent);
            var bounds = new Bounds(objects[i].transform.position, Vector3.zero);
            for (i = 1; i < length; i++)
                bounds.Encapsulate(objects[i].transform.position);
            parent.transform.position = bounds.center;
            for (i = 0; i < length; i++)
                objects[i].transform.SetParent(parent.transform, true);
            Selection.activeGameObject = parent;
        }
    }
}