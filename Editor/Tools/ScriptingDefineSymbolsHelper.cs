using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace MyGameDevTools.Extensions
{
    public static class ScriptingDefineSymbolsHelper
    {
        public static bool HasScriptingDefineSymbol(string define) => GetScriptingDefineSymbols(out _).Contains(define);

        public static void AddScriptingDefineSymbol(string define)
        {
            if (HasScriptingDefineSymbol(define))
                throw new System.ArgumentException($"The Scripting Define Symbol \"{define}\" is already set in the Project Settings and therefore cannot be added again.", nameof(define));

            var defines = GetScriptingDefineSymbols(out var namedBuildTarget);
            var newDefines = new List<string>();
            newDefines.AddRange(defines);
            newDefines.Add(define);
            SetScriptingDefineSymbols(newDefines.ToArray(), namedBuildTarget);
        }

        public static void RemoveScriptingDefineSymbol(string define)
        {
            if (!HasScriptingDefineSymbol(define))
                throw new System.ArgumentException($"The Scripting Define Symbol \"{define}\" is not set in the Project Settings and therefore cannot be removed.", nameof(define));

            var defines = GetScriptingDefineSymbols(out var namedBuildTarget);
            var newDefines = new List<string>();
            newDefines.AddRange(defines);
            newDefines.Remove(define);
            SetScriptingDefineSymbols(newDefines.ToArray(), namedBuildTarget);

        }

        static void SetScriptingDefineSymbols(string[] defines, NamedBuildTarget namedBuildTarget)
        {
            PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, defines);
        }

        static string[] GetScriptingDefineSymbols(out NamedBuildTarget namedBuildTarget)
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            var group = BuildPipeline.GetBuildTargetGroup(target);
            namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(group);
            PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out var defines);
            return defines;
        }
    }
}