/**
 * GameObjectExtensions.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 1/24/2021 (en-US)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameDevTools.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetChildrenLayerRecursively(this Transform transform, int layer)
        {
            transform.gameObject.layer = layer;
            if (transform.childCount > 0)
            {
                var length = transform.childCount;
                for (int i = 0; i < length; i++)
                    transform.GetChild(i).SetChildrenLayerRecursively(layer);
            }
        }

        public static void DestroyAllChildrenComponentsOfType<T>(this GameObject gameObject, bool includeInactive = false) where T : Component => ObjectExtensions.DestroyObjects(gameObject.GetComponentsInChildren<T>(includeInactive));

        public static void DestroyAllComponentsOfType<T>(this GameObject gameObject) where T : Component => ObjectExtensions.DestroyObjects(gameObject.GetComponents<T>());

        public static Coroutine DelayCallInPhysicsFrames(this MonoBehaviour monoBehaviour, int frames, System.Action call)
        {
            return monoBehaviour.StartCoroutine(delayCallInPhysicsFramesRoutine());
            IEnumerator delayCallInPhysicsFramesRoutine()
            {
                var delay = new WaitForFixedUpdate();
                for (int i = 0; i < frames; i++)
                    yield return delay;
                call?.Invoke();
            }
        }

        public static Coroutine DelayCallInFrames(this MonoBehaviour monoBehaviour, int frames, System.Action call)
        {
            return monoBehaviour.StartCoroutine(delayCallInFramesRoutine());
            IEnumerator delayCallInFramesRoutine()
            {
                var delay = new WaitForEndOfFrame();
                for (int i = 0; i < frames; i++)
#if UNITY_EDITOR
                    yield return null;
#else
                    yield return delay;
#endif
                call?.Invoke();
            }
        }

        public static Coroutine DelayCall(this MonoBehaviour monoBehaviour, float delaySeconds, System.Action call, bool realtime = false)
        {
            return monoBehaviour.StartCoroutine(delayCallRoutine());
            IEnumerator delayCallRoutine()
            {
                yield return realtime ? new WaitForSecondsRealtime(delaySeconds) : new WaitForSeconds(delaySeconds);
                call?.Invoke();
            }
        }

        public static List<GameObject> GetRootGameObjects(bool allScenes = false)
        {
            var rootObjects = new List<GameObject>();
            if (allScenes)
            {
                var sceneCount = SceneManager.sceneCount;
                for (int i = 0; i < sceneCount; i++)
                    rootObjects.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());
            }
            else
                SceneManager.GetActiveScene().GetRootGameObjects(rootObjects);

            return rootObjects;
        }

        public static bool HasLayer(this LayerMask mask, int layer) => (1 << layer | mask) == mask;
    }
}