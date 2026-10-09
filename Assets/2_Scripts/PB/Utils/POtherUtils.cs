using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
#endif

namespace PB.UTILS
{
    public static class POtherUtils
    {
        public static void AddSymbol(string symbol)
        {
#if UNITY_EDITOR
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            NamedBuildTarget targetGroup = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
            string defines = PlayerSettings.GetScriptingDefineSymbols(targetGroup);

            if (!defines.Contains(symbol))
            {
                if (!string.IsNullOrEmpty(defines)) defines += ";";
                defines += symbol;
                PlayerSettings.SetScriptingDefineSymbols(targetGroup, defines);
                Debug.Log($"{symbol} added to scripting define symbols for {targetGroup}");
            }
#endif
        }
    }
}