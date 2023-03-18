/**
 * ObjectCleanupTestBase.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 2023-03-18
 */

using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using UnityEngine.TestTools;
using System.Collections;

namespace MyGameDevTools.Extensions.Tests
{
    public class ObjectCleanupTestBase
    {
        protected GameObject[] _rootGameObjects;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        }

        [UnityTearDown]
        public IEnumerator CleanupGameObjects()
        {
            var objectsToDestroy = SceneManager.GetActiveScene().GetRootGameObjects().Except(_rootGameObjects).ToArray();
            for (int i = objectsToDestroy.Length - 1; i >= 0; i--)
                Object.Destroy(objectsToDestroy[i]);

            yield return null;
        }
    }
}