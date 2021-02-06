/**
 * LabeledArrayAttribute.cs
 * Created by: João Borks [joao.borks@gmail.com]
 * Created on: 12/28/2017 (en-US)
 * Reference from John Avery: https://forum.unity.com/threads/how-to-change-the-name-of-list-elements-in-the-inspector.448910/
 */

using UnityEngine;
using System;

namespace UnityTools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public sealed class LabeledArrayAttribute : PropertyAttribute
    {
        public readonly string[] names;
        public LabeledArrayAttribute(string[] names) { this.names = names; }
        public LabeledArrayAttribute(Type enumType) { names = Enum.GetNames(enumType); }
    }
}