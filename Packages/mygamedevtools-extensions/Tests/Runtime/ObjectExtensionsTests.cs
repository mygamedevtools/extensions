/**
 * ObjectExtensionsTests.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2023-03-18
 */

using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;
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