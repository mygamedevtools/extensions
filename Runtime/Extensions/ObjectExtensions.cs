/**
 * ObjectExtensions.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 1/24/2021 (en-US)
 */

using System.Collections.Generic;
using UnityEngine;

namespace MyGameDevTools.Extensions
{
    public static class ObjectExtensions
    {
        public static T FindImplementationOfType<T>(this Object requestObject, bool includeInactive = false, bool searchAllScenes = false)
        {
            return FindImplementationOfType<T>(includeInactive, searchAllScenes);
        }
        public static T FindImplementationOfType<T>(bool includeInactive = false, bool searchAllScenes = false)
        {
            TryFindImplementationOfType<T>(out var implementation, includeInactive, searchAllScenes);
            return implementation;
        }

        public static bool TryFindImplementationOfType<T>(this Object requestObject, out T implementation, bool includeInactive = false, bool searchAllScenes = false)
        {
            return TryFindImplementationOfType(out implementation, includeInactive, searchAllScenes);
        }
        public static bool TryFindImplementationOfType<T>(out T implementation, bool includeInactive = false, bool searchAllScenes = false)
        {
            var rootObjects = GameObjectExtensions.GetRootGameObjects(searchAllScenes);

            implementation = default;
            foreach (var obj in rootObjects)
            {
                implementation = obj.GetComponentInChildren<T>(includeInactive);
                if (implementation != null)
                    return true;
            }
            return false;

        }

        public static List<T> FindImplementationsOfType<T>(this Object requestObject, bool includeInactive = false, bool searchAllScenes = false)
        {
            return FindImplementationsOfType<T>(includeInactive, searchAllScenes);
        }
        public static List<T> FindImplementationsOfType<T>(bool includeInactive = false, bool searchAllScenes = false)
        {
            var rootObjects = GameObjectExtensions.GetRootGameObjects(searchAllScenes);

            var implementations = new List<T>();
            foreach (var obj in rootObjects)
            {
                var childImplementations = obj.GetComponentsInChildren<T>(includeInactive);
                if (childImplementations != null)
                    implementations.AddRange(childImplementations);
            }

            return implementations;
        }

        public static void DestroyObjects(Object[] objects)
        {
            var length = objects.Length;
            for (int i = 0; i < length; i++)
                Object.Destroy(objects[i]);
        }
    }
}