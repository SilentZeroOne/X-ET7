#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace ET
{
    public class FixBurstCompiler : IPreprocessBuildWithReport
    {
        public int callbackOrder
        {
            get => 0;
        }
        public void OnPreprocessBuild(BuildReport report)
        {
#if UNITY_2021_3_OR_NEWER
            var ndkRoot = EditorPrefs.GetString("AndroidNdkRootR16b");

            Environment.SetEnvironmentVariable("ANDROID_NDK_ROOT", ndkRoot);
#endif
        }
    }
}

#endif