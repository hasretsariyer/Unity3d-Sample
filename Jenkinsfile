pipeline { 
  
   agent any
   
   parameters {
    choice(name: 'build_type', choices: 'Release\nDebug', description: 'Select build type')
    file(name: 'uploaded_file', description: 'archive')
  }

   stages {
   
     stage('Parse Mobile Provision Profile') { 
        steps { 
          sh '''
           security cms -D -i $uploaded_file >> temp.plist
           env.PROVISIONING_PROFILE_SPECIFIER=$(/usr/libexec/PlistBuddy -c 'print ":Name"' temp.plist)
           env.UUID=$(/usr/libexec/PlistBuddy -c 'print ":UUID"' temp.plist)
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
