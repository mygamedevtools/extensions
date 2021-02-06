/**
 * ReadOnlyAttribute.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 6/5/2018 (en-US)
 * Reference from It3ration: https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
 */

using UnityEngine;
using System;

namespace MyUnityTools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public sealed class ReadOnlyAttribute : PropertyAttribute { }
}