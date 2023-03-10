/**
 * GameObjectExtensionsTests.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2023-03-09
 */

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameDevTools.Extensions.Tests
{
    public class GameObjectExtensionsTests
    {
        GameObject[] _rootGameObjects;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        }

        [TearDown]
        public void TearDown()
        {
            var objectsToDestroy = SceneManager.GetActiveScene().GetRootGameObjects().Except(_rootGameObjects).ToArray();
            for (int i = objectsToDestroy.Length - 1; i >= 0; i--)
                Object.Destroy(objectsToDestroy[i]);
        }

        [Test]
        public void SetChildrenLayer()
        {
            var totalObjectCount = 10;
            var parentCount = 3;

            var allObjects = new List<GameObject>(totalObjectCount);

            int i;
            for (i = 0; i < totalObjectCount; i++)
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

            for (i = totalObjectCount - parentCount - 1; i >= 0; i--)
            {
                var index = Random.Range(0, parentCount);
                objects[i].transform.SetParent(parents[index].transform);
                objects.RemoveAt(i);
            }

            for (i = parentCount - 1; i > 0; i--)
            {
                parents[i].transform.SetParent(parents[i - 1].transform);
            }

            parents[0].transform.SetChildrenLayerRecursively(1);

            foreach (var o in allObjects)
                Assert.AreEqual(1, o.layer);
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
    }
}