# Unity3d Build in Jenkins

# Installations 

- Install Jenkins: https://www.jenkins.io/download/lts/macos/
- `brew services restart jenkins-lts`
- After starting the Jenkins service, browse to http://localhost:8080 

- Install Jenkins Plugins
    - Unity3d
    - Generic Webhook Trigger Plugin

- Install ngrok to expose a local web server to the internet

# Jenkins + Webhook Integration

- Start ngrok via `ngrok  http 8080`
- <img width="810" alt="Screen Shot 2021-10-27 at 20 24 11" src="https://user-images.githubusercontent.com/20642286/139115629-80c13dc9-f4bb-47ec-8b12-d84a863d694d.png">

- Set webhook in github<img width="1257" alt="Screen Shot 2021-10-27 at 19 08 33" src="https://user-images.githubusercontent.com/20642286/139115367-8de48306-0512-4383-804d-b1e32f1207b3.png">

# Automated Unity Build in iOS

Create an [editor method](https://github.com/hasretsariyer/Unity3d-Sample/blob/dev-1/Assets/Editor/ExportTool.cs) in Unity that does the iOS building 

```
using UnityEditor;
using System.Collections.Generic;

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

```

# Parameters

<img width="886" alt="Screen Shot 2021-10-27 at 20 34 28" src="https://user-images.githubusercontent.com/20642286/139117060-e6edd17d-0a0d-4782-8b2d-1c24b95fb86d.png">

## File upload issue:
If i look in the workspace of the job, the file which was selected as a parameter hasn't bee uploaded into the workspace, this is the bug.

https://issues.jenkins.io/browse/JENKINS-47333
https://github.com/jenkins-infra/jenkins.io/pull/2388
https://github.com/MarkEWaite/jenkins-bugs/blob/JENKINS-47333/Jenkinsfile#L11

Alternative solution: Added string parameter

# Stages

```

    stages {
        stage('iOS Build') {
            steps {
              ...
            }
        }
        stage('iOS Archive') {
            steps {
              ...
            }
        }

        stage('iOS iPA & Update exportOptions.plist') {
            steps {
              ...
            }
        }
    }
    post {
        always {
            archiveArtifacts artifacts: 'outputFolder/**/*.*'
        }
        success {
            script {
              ...
            }
        }
    }
}
```
# Additional Steps

ipa file is distributed via appcircle cli after successful build.

Download appcircle cli -> https://github.com/appcircleio/appcircle-cli

<img width="1073" alt="Screen Shot 2021-10-27 at 20 48 29" src="https://user-images.githubusercontent.com/20642286/139124352-81f140a8-83e6-4bf4-8bab-5a7026154948.png">


# Jenkins Stage View


<img width="865" alt="Screen Shot 2021-10-27 at 21 21 45" src="https://user-images.githubusercontent.com/20642286/139124299-fa2d1909-d84d-4601-a6fb-5cde7ffd706e.png">

