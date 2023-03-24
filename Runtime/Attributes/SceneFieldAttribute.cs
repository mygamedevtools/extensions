/**
 * SceneField.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 6/11/2020 (en-US)
 */

using System;
using UnityEngine;

namespace MyGameDevTools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public sealed class SceneFieldAttribute : PropertyAttribute { }
}