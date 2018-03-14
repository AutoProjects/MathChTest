// C# example
using UnityEditor;
using System.IO;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[InitializeOnLoad]
public class PreloadSigningAlias
{

    static PreloadSigningAlias()
    {
        PlayerSettings.Android.keystorePass = "111111";
        PlayerSettings.Android.keyaliasName = "math.alias";
        PlayerSettings.Android.keyaliasPass = "111111";
    }

}

class PerformBuild
{
static void CurrentVersion(){
  PlayerSettings.bundleVersion = "0.5.0"; // delete after resolving Version issue
  /*var currentVersion = typeof(UnityEditor.Ver).Assembly.GetName().Version;

		PlayerSettings.bundleVersion = string.Format("{0}.{1}.{2}",
      currentVersion.Major,
			currentVersion.Minor,
			currentVersion.Build);

		PlayerSettings.iOS.buildNumber = currentVersion.Revision.ToString();
		PlayerSettings.Android.bundleVersionCode = currentVersion.Revision;*/
}
    static string[] GetBuildScenes()
	{
		List<string> names = new List<string>();

		foreach(EditorBuildSettingsScene e in EditorBuildSettings.scenes)
		{
			if(e==null)
				continue;

			if(e.enabled)
				names.Add(e.path);
		}
		return names.ToArray();
	}

	static string GetBuildPath()
	{
		return "/Users/jenkins/Documents/Math_build/iPhone";
	}

	[UnityEditor.MenuItem("CUSTOM/Test Command Line Build Step")]
	static void CommandLineBuild ()
	{
    PlayerSettings.iOS.buildNumber = "2";
		Debug.Log("Command line build\n------------------\n------------------");

		string[] scenes = GetBuildScenes();
		string path = GetBuildPath();
		if(scenes == null || scenes.Length==0 || path == null)
			return;

		Debug.Log(string.Format("Path: \"{0}\"", path));
		for(int i=0; i<scenes.Length; ++i)
		{
			Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
		}

		Debug.Log("Starting Build!");
		BuildPipeline.BuildPlayer(scenes, path, BuildTarget.iOS, BuildOptions.None);
	}

    static string GetBuildPathAndroid()
	{
		return "/Users/jenkins/Documents/Math_build/android";
	}

	[UnityEditor.MenuItem("CUSTOM/Test Command Line Build Step Android")]
	static void CommandLineBuildAndroid ()
	{
    CurrentVersion();
		Debug.Log("Command line build android version\n------------------\n------------------");

		string[] scenes = GetBuildScenes();
		string path = GetBuildPathAndroid();
		if(scenes == null || scenes.Length==0 || path == null)
			return;

		Debug.Log(string.Format("Path: \"{0}\"", path));
		for(int i=0; i<scenes.Length; ++i)
		{
			Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
		}

		Debug.Log("Starting Android Build!");
		BuildPipeline.BuildPlayer(scenes, path, BuildTarget.Android, BuildOptions.None);
	}
}


/*
/Applications/Unity5.6.4/Unity.app/Contents/MacOS/Unity \
-projectPath /Users/jenkins/Documents/MathCh \
-quit \
-batchmode \
-logfile \
-buildTarget android \
-stackTraceLogType Full \
-executeMethod PerformBuild.CommandLineBuildAndroid
*/
