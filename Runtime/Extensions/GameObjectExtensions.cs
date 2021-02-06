/**
 * GameObjectExtensions.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 1/24/2021 (en-US)
 */

using System.Collections;
using UnityEngine;

namespace MyUnityTools.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetChildrenLayerRecursevely(this Transform transform, int layer)
        {
            transform.gameObject.layer = layer;
            if (transform.childCount > 0)
            {
                var length = transform.childCount;
                for (int i = 0; i < length; i++)
                    transform.GetChild(i).SetChildrenLayerRecursevely(layer);
            }
        }

        public static void DestroyAllChildrenComponentsOfType<T>(this GameObject gameObject, bool includeInactive = false) where T : Component => DestroyObjects(gameObject.GetComponentsInChildren<T>(includeInactive));

        public static void DestroyAllComponentsOfType<T>(this GameObject gameObject) where T : Component => DestroyObjects(gameObject.GetComponents<T>());

        public static void DestroyObjects(Object[] objects)
        {
            var length = objects.Length;
            for (int i = 0; i < length; i++)
                Object.Destroy(objects[i]);
        }

        public static Coroutine DelayedCall(this MonoBehaviour monoBehaviour, float delay, System.Action call)
        {
            return monoBehaviour.StartCoroutine(delayedCallRoutine());
            IEnumerator delayedCallRoutine()
            {
                yield return new WaitForSeconds(delay);
                call?.Invoke();
            }
        }

        public static bool HasLayer(this LayerMask mask, int layer) => (1 << layer | mask) == mask;
    }
}