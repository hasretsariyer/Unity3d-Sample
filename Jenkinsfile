@Library('jenkins-shared-lib-example') _

pipeline {
    agent any
    parameters {
        choice(name: 'build_type', choices: 'Release\nDebug', description: 'Select build type')
        file(name: 'uploaded_file', description: 'archive')
        string(name: 'provisioning_profile_path', defaultValue: '../provisioning-profiles/test.mobileprovision', description: 'Enter your provisioning profile file path')
    }

    stages {
        stage("iOS Unity Build") {     
            iOSUnityBuild pwd(), "Release"
        }

        stage("iOS Archive") {     
            iOSArchive $provisioning_profile_path, "./iOSProj/Unity-iPhone.xcodeproj", "./outputFolder/jenkins-test.xcarchive"
        }
    }
}
