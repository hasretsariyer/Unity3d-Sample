pipeline { 
  
   agent any
   
   parameters {
    choice(name: 'build_type', choices: 'Release\nDebug', description: 'Select build type')
    file(name: 'uploaded_file', description: 'archive')
    string(name: 'provisioning_profile_path', defaultValue: '', description: 'Enter your provisioning profile file path')
  }

   stages {
   
     stage('Parse Mobile Provision Profile') { 
        steps { 
          sh '''
           # https://issues.jenkins.io/browse/JENKINS-47333
           # https://github.com/jenkins-infra/jenkins.io/pull/2388
           # https://github.com/MarkEWaite/jenkins-bugs/blob/JENKINS-47333/Jenkinsfile#L11
           
           security cms -D -i $provisioning_profile_path >> temp.plist
           PROVISIONING_PROFILE_SPECIFIER="$(/usr/libexec/PlistBuddy -c 'print ":Name"' temp.plist)"
           UUID="$(/usr/libexec/PlistBuddy -c 'print ":UUID"' temp.plist)"
           env.profile_Specifier = $PROVISIONING_PROFILE_SPECIFIER
           env.uuid = $UUID
           '''
        }
     }
     
     stage('iOS Build') {
          steps {
               sh '''
                rm -rf Builds
                echo "Unity Build starting..."
                /Applications/Unity/Hub/Editor/2020.3.20f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${PWD} -executeMethod "ExportTool.ExportXcodeProject" -buildType $build_type -logFile export.log
                echo "Unity Build finished..."
                '''
          }
      }
     stage('iOS Archive') {
        steps {
            sh '''
            echo "Create Archive starting..."
            echo "${env.PROVISIONING_PROFILE_SPECIFIER}"
            pwd
            /usr/bin/xcodebuild -project ./iOSProj/Unity-iPhone.xcodeproj -scheme  Unity-iPhone  -configuration Release -archivePath jenkins-test.xcarchive archive
            echo "Create Archive finished..."
            '''
        }
    }
     
     stage('Test') { 
        steps { 
           sh 'echo "testing application..."'
        }
      }

         stage("Deploy application") { 
         steps { 
           sh 'echo "deploying application..."'
         }

     }
  
   	}

   }
