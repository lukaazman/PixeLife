using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

public static class BuildScript
{
    private const string BuildPath = "build/WebGL";

    public static void BuildWebGL()
    {
        Directory.CreateDirectory(BuildPath);
        var scenes = EditorBuildSettings.scenes;
        if (scenes.Length == 0)
        {
            throw new BuildFailedException("No scenes are enabled in the Unity build settings.");
        }

        var report = BuildPipeline.BuildPlayer(scenes, BuildPath, BuildTarget.WebGL, BuildOptions.StrictMode);
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            throw new BuildFailedException("WebGL build failed: " + report.summary.result);
        }

        File.WriteAllText(Path.Combine(BuildPath, ".nojekyll"), string.Empty);
    }
}


