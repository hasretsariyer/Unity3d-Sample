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
