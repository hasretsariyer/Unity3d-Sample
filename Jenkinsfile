pipeline { 
  
   agent any

   stages {
   
     stage('Install Dependencies') { 
        steps { 
           sh 'echo Download some dependencies' 
        }
     }
     
     stage('iOS Build') {
          steps {
               sh '''
                rm -rf Builds
                echo "Unity Build starting..."
                /Applications/Unity/Hub/Editor/2020.3.20f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath "/Users/hasretsariyer/Unity-Sample1" -executeMethod "ExportTool.ExportXcodeProject" -logFile export.log
                echo "Unity Build finished..."
                '''
          }
      }
     stage('iOS Archive') {
        steps {
            sh '''
            echo "Create Archive starting..."
            pwd
            /usr/bin/xcodebuild -project ./Unity-iPhone.xcodeproj -scheme  Unity-iPhone  -configuration Release -archivePath jenkins-test.xcarchive archive
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
