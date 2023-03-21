/**
 * GameObjectExtensionsTests.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2023-03-09
 */

using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MyGameDevTools.Extensions.Tests
{
    public class GameObjectExtensionsTests : ObjectCleanupTestBase
    {
        [Test]
        public void SetChildrenLayer()
        {
            BuildRandomGameObjectHierarchy(10, 3, out var hierarchy, out var root);

            Assert.AreEqual(0, root.layer);
            root.transform.SetChildrenLayerRecursively(1);

            foreach (var o in hierarchy)
                Assert.AreEqual(1, o.layer);
        }

        [UnityTest]
        public IEnumerator DestroyAllChildrenComponentsOfType()
        {
            BuildRandomGameObjectHierarchy(10, 1, out var hierarchy, out var root);

            root.DestroyAllChildrenComponentsOfType<Collider>();

            yield return null; // Objects are destroyed at the end of the current update loop

            foreach (var o in hierarchy)
                Assert.Null(o.GetComponent<Collider>());
        }

        [UnityTest]
        public IEnumerator DestroyAllComponentsOfType()
        {
            var gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

            var componentCount = 3;
            for (int i = 0; i < componentCount; i++)
                gameObject.AddComponent<SphereCollider>();

            Assert.AreEqual(componentCount, gameObject.GetComponents<SphereCollider>().Length);

            gameObject.DestroyAllComponentsOfType<SphereCollider>();

            yield return null; // Objects are destroyed at the end of the current update loop

            Assert.AreEqual(0, gameObject.GetComponents<SphereCollider>().Length);
        }

        [UnityTest]
        public IEnumerator DelayCallInPhysicsFrames()
        {
            var behavior = new GameObject().AddComponent<TestBehavior>();

            bool set = false;
            behavior.DelayCallInPhysicsFrames(2, () => set = true);

            var interval = new WaitForFixedUpdate();

            yield return interval;

            Assert.IsFalse(set);

            yield return interval;

            Assert.IsTrue(set);
        }

        [UnityTest]
        public IEnumerator DelayCallInFrames()
        {
            var behavior = new GameObject().AddComponent<TestBehavior>();

            bool set = false;
            behavior.DelayCallInFrames(2, () => set = true);

            yield return null;

            Assert.IsFalse(set);

            yield return null;

            Assert.IsTrue(set);
        }

        [UnityTest]
        public IEnumerator DelayCall()
        {
            var behavior = new GameObject().AddComponent<TestBehavior>();

            bool set = false;
            behavior.DelayCall(1, () => set = true);

            var interval = new WaitForSeconds(0.5f);

            yield return interval;

            Assert.IsFalse(set);

            yield return interval;

            Assert.IsTrue(set);
        }

        [Test]
        public void HasMask()
        {
            var mask = new LayerMask
            {
                value = LayerMask.GetMask("TransparentFX", "UI")
            };

            Assert.True(mask.HasLayer(LayerMask.NameToLayer("TransparentFX")));
            Assert.True(mask.HasLayer(LayerMask.NameToLayer("UI")));
            Assert.False(mask.HasLayer(LayerMask.NameToLayer("Ignore Raycast")));
        }

        [UnityTest]
        public IEnumerator GetRootGameObjects()
        {
            var tempScene = SceneManager.CreateScene("temp");

            var newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            SceneManager.MoveGameObjectToScene(newObject, tempScene);

            var activeSceneRootObjects = GameObjectExtensions.GetRootGameObjects();
            Assert.AreEqual(activeSceneRootObjects.Count, _rootGameObjects.Length);
            for (int i = 0; i < activeSceneRootObjects.Count; i++)
                Assert.AreEqual(_rootGameObjects[i], activeSceneRootObjects[i]);

            var allScenesRootObjects = GameObjectExtensions.GetRootGameObjects(true);
            Assert.AreEqual(allScenesRootObjects.Count, _rootGameObjects.Length + 1);

            yield return SceneManager.UnloadSceneAsync(tempScene);
        }

        public void BuildRandomGameObjectHierarchy(int objectCount, int parentCount, out GameObject[] hierarchy, out GameObject root)
        {
            Assert.NotZero(objectCount);
            Assert.NotZero(parentCount);
            Assert.Greater(objectCount, parentCount);

            var allObjects = new List<GameObject>(objectCount);

            int i;
            for (i = 0; i < objectCount; i++)
                allObjects.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));

            var objects = new List<GameObject>(allObjects);

            Assert.AreEqual(0, objects[0].layer);

            var parents = new GameObject[parentCount];
            for (i = 0; i < parentCount; i++)
            {
                var index = Random.Range(0, objects.Count);
                parents[i] = objects[index];
                objects.RemoveAt(index);
            }

            for (i = objectCount - parentCount - 1; i >= 0; i--)
            {
                var index = Random.Range(0, parentCount);
                objects[i].transform.SetParent(parents[index].transform);
                objects.RemoveAt(i);
            }

            for (i = parentCount - 1; i > 0; i--)
            {
                parents[i].transform.SetParent(parents[i - 1].transform);
            }

            hierarchy = allObjects.ToArray();
            root = parents[0];
        }
    }

    public class TestBehavior : MonoBehaviour { }

    public class TestInterfaceImplementor : MonoBehaviour, ITestInterface { }

    public interface ITestInterface { }
}