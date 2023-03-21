/**
 * ObjectExtensionsTests.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2023-03-18
 */

using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MyGameDevTools.Extensions.Tests
{
    public class ObjectExtensionsTests : ObjectCleanupTestBase
    {
        [Test]
        public void FindImplementationOfType()
        {
            Assert.False(ObjectExtensions.TryFindImplementationOfType<ITestInterface>(out _));
            Assert.Null(ObjectExtensions.FindImplementationOfType<ITestInterface>());

            var implementation = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<TestInterfaceImplementor>();

            Assert.True(implementation.TryFindImplementationOfType<ITestInterface>(out var implementationFound));
            Assert.NotNull(implementation.FindImplementationOfType<ITestInterface>());
            Assert.AreEqual(implementation, implementationFound);

            implementation.enabled = false;

            Assert.True(implementation.TryFindImplementationOfType<ITestInterface>(out implementationFound, true));
            Assert.NotNull(implementation.FindImplementationOfType<ITestInterface>(true));
            Assert.AreEqual(implementation, implementationFound);
        }

        [UnityTest]
        public IEnumerator FindImplementationOfType_AllScenes()
        {
            var tempScene = SceneManager.CreateScene("temp");

            Assert.False(ObjectExtensions.TryFindImplementationOfType<ITestInterface>(out _, false, true));
            Assert.Null(ObjectExtensions.FindImplementationOfType<ITestInterface>(false, true));

            var parent = new GameObject();
            var implementation = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<TestInterfaceImplementor>();
            implementation.transform.SetParent(parent.transform);
            SceneManager.MoveGameObjectToScene(parent, tempScene);

            Assert.True(implementation.TryFindImplementationOfType<ITestInterface>(out var implementationFound, false, true));
            Assert.NotNull(implementation.FindImplementationOfType<ITestInterface>(false, true));
            Assert.AreEqual(implementation, implementationFound);

            implementation.gameObject.SetActive(false);

            Assert.True(implementation.TryFindImplementationOfType<ITestInterface>(out implementationFound, true, true));
            Assert.NotNull(implementation.FindImplementationOfType<ITestInterface>(true, true));
            Assert.AreEqual(implementation, implementationFound);

            Assert.False(implementation.TryFindImplementationOfType<ITestInterface>(out implementationFound, false, false));
            Assert.Null(implementation.FindImplementationOfType<ITestInterface>(false, false));

            yield return SceneManager.UnloadSceneAsync(tempScene);
        }

        [Test]
        public void FindImplementationsOfType()
        {
            Assert.Zero(ObjectExtensions.FindImplementationsOfType<ITestInterface>().Count);

            var parent = new GameObject();
            var objects = 3;
            var implementations = new TestInterfaceImplementor[objects];
            for (int i = 0; i < objects; i++)
            {
                implementations[i] = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<TestInterfaceImplementor>();
                implementations[i].transform.SetParent(parent.transform);
            }

            implementations[1].gameObject.SetActive(false);

            Assert.AreEqual(objects, ObjectExtensions.FindImplementationsOfType<ITestInterface>(true).Count);
            Assert.AreEqual(objects - 1, ObjectExtensions.FindImplementationsOfType<ITestInterface>().Count);
        }

        [UnityTest]
        public IEnumerator FindImplementationsOfType_AllScenes()
        {
            var tempScene = SceneManager.CreateScene("temp");

            Assert.Zero(ObjectExtensions.FindImplementationsOfType<ITestInterface>().Count);

            var parent = new GameObject();
            var objects = 3;
            var implementations = new TestInterfaceImplementor[objects];
            for (int i = 0; i < objects; i++)
            {
                implementations[i] = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<TestInterfaceImplementor>();
                implementations[i].transform.SetParent(parent.transform);
            }
            SceneManager.MoveGameObjectToScene(parent, tempScene);

            implementations[1].gameObject.SetActive(false);

            Assert.AreEqual(objects, ObjectExtensions.FindImplementationsOfType<ITestInterface>(true, true).Count);
            Assert.AreEqual(objects - 1, ObjectExtensions.FindImplementationsOfType<ITestInterface>(false, true).Count);
            Assert.AreEqual(0, ObjectExtensions.FindImplementationsOfType<ITestInterface>(false, false).Count);

            yield return SceneManager.UnloadSceneAsync(tempScene);
        }

        [UnityTest]
        public IEnumerator DestroyObjects()
        {
            var objectCount = 10;
            for (int i = 0; i < objectCount; i++)
                GameObject.CreatePrimitive(PrimitiveType.Cube);

            var objects = Object.FindObjectsOfType<MeshFilter>().Select(m => m.gameObject).ToArray();
            ObjectExtensions.DestroyObjects(objects);

            yield return null; // Objects are destroyed at the end of the current update loop

            Assert.AreEqual(0, Object.FindObjectsOfType<MeshFilter>().Length);
        }
    }
}