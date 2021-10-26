using UnityEditor;
using System.Collections.Generic;

// Put this memu command script into Assets/Editor/

class ExportTool {
    static void ExportXcodeProject() {

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);

        EditorUserBuildSettings.symlinkLibraries = true;
        EditorUserBuildSettings.development = true;
        EditorUserBuildSettings.allowDebugging = true;

        string[] args = System.Environment.GetCommandLineArgs();
        string buildType = "";
        for (int i = 0; i < args.Length; i++) {
            if (args[i] == "-buildType") {
                buildType = args[i + 1];
                if (buildType == "Debug") {
                    EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Debug;
                } else {
                    EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Release;
                }
            }
        }

        List < string > scenes = new List < string > ();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++) {
            if (EditorBuildSettings.scenes[i].enabled) {
                scenes.Add(EditorBuildSettings.scenes[i].path);
            }
        }

        BuildPipeline.BuildPlayer(scenes.ToArray(), "iOSProj", BuildTarget.iOS, BuildOptions.None);
    }
}