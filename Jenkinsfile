pipeline {

    agent any

    parameters {
        choice(name: 'build_type', choices: 'Release\nDebug', description: 'Select build type')
        file(name: 'uploaded_file', description: 'archive')
        string(name: 'provisioning_profile_path', defaultValue: '../provisioning-profiles/test.mobileprovision', description: 'Enter your provisioning profile file path')
    }

    stages {
        stage('iOS Build') {
            steps {
               sh '''
                rm -rf outputFolder
                mkdir outputFolder
                
                echo "Unity Build starting..."
                /Applications/Unity/Hub/Editor/2020.3.20f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${PWD} -executeMethod "ExportTool.ExportXcodeProject" -buildType $build_type -logFile ./outputFolder/export.log
                echo "Unity Build finished..."
                '''
            }
        }
        stage('iOS Archive') {
            steps {
                sh '''
                echo "Create Archive starting..."

               # https://issues.jenkins.io/browse/JENKINS-47333
               # https://github.com/jenkins-infra/jenkins.io/pull/2388
               # https://github.com/MarkEWaite/jenkins-bugs/blob/JENKINS-47333/Jenkinsfile#L11

                security cms -D -i $provisioning_profile_path >> temp.plist
                PROVISIONING_PROFILE_SPECIFIER="$(/usr/libexec/PlistBuddy -c 'print ":Name"' temp.plist)"
                UUID="$(/usr/libexec/PlistBuddy -c 'print ":UUID"' temp.plist)"
                declare -a TEAM_ID=($(/usr/libexec/PlistBuddy -c 'print ":TeamIdentifier"' temp.plist | sed -e 1d -e '$d'))

                ruby updateExportOptions.rb ./temp.plist ./exportOptions.plist
                cat ./exportOptions.plist
                
                /usr/bin/xcodebuild -project ./iOSProj/Unity-iPhone.xcodeproj -scheme Unity-iPhone -sdk iphoneos -configuration Release archive -archivePath ./outputFolder/jenkins-test.xcarchive clean CODE_SIGN_STYLE=Manual  COMPILER_INDEX_STORE_ENABLE=NO CODE_SIGN_IDENTITY="iPhone Distribution" PROVISIONING_PROFILE=$UUID PROVISIONING_PROFILE_SPECIFIER="$PROVISIONING_PROFILE_SPECIFIER" DEVELOPMENT_TEAM=$TEAM_ID EXPANDED_CODE_SIGN_IDENTITY="" CODE_SIGNING_REQUIRED="NO" CODE_SIGNING_ALLOWED="NO"

                echo "Create Archive finished..."
                '''
            }
        }

        stage('iOS iPA') {
            steps {
                sh '''
                echo "Export ipa starting..."
                /usr/bin/xcodebuild -exportArchive -archivePath jenkins-test.xcarchive -exportPath ./outputFolder -exportOptionsPlist exportOptions.plist
                echo "Export ipa finished..."
                '''
            }
        }
    }
    post {
        always {
            archiveArtifacts artifacts: 'outputFolder/**/*.*'
        }
        success {
            script {
                sh '''
                echo "Deploy to Appcircle"
                pwd
                echo $BUILD_URL
                appcircle upload --app=./Myproject1.ipa --profileId=68d1bee0-2e98-4213-9d2f-0f7529aeb74b --message="Build Url->$BUILD_URL"
                '''
            }
        }
    }
}
