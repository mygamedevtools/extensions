/**
* DebugExtensionsTests.cs
* Created by: João Borks [joao.borks@gmail.com]
* Created on: 2023-03-18
*/

using NUnit.Framework;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.TestTools;

namespace MyGameDevTools.Extensions.Tests
{
    public class DebugExtensionsTests
    {
        [Test]
        public void LogCollection()
        {
            var array = new float[42];
            LogAssert.Expect(LogType.Log, new Regex("collection with 42 elements"));
            DebugExtensions.LogCollection(array);
        }

        [Test]
        public void LogJaggedArray()
        {
            int arrayCount = 4;
            var jaggedArray = new float[arrayCount][];
            for (int i = 0; i < arrayCount; i++)
                jaggedArray[i] = new float[Random.Range(4, 16)];

            LogAssert.Expect(LogType.Log, new Regex("(jagged array with 4 arrays)|(array with \\d elements)"));
            DebugExtensions.LogJaggedArray(jaggedArray);
        }
    }
}