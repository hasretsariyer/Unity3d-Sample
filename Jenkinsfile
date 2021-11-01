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
            steps {
                iOSUnityBuild pwd(), params.build_type, "./builds"
            }
        }

        stage("iOS Archive") {       
            steps {
                iOSArchive params.provisioning_profile_path, "./builds/jenkins-unity.xcarchive"
            }
        }

        stage("iOS IPA Export") {       
            steps {
                iOSExportIPA "./builds/jenkins-unity.xcarchive", "./builds"
            }
        }
    }
    post {
        always {
            archiveArtifacts artifacts: 'builds/**/*.*'
        }
    }
}
