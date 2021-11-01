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
                iOSUnityBuild pwd(), params.build_type
            }
        }

        stage("iOS Archive") {       
            steps {
                iOSArchive params.provisioning_profile_path, "./outputFolder/jenkins-test.xcarchive"
            }
        }

        stage("IPA Export") {       
            steps {
                iOSExportIPA "./outputFolder/jenkins-test.xcarchive", "./outputFolder"
            }
        }
    }
    post {
        always {
            archiveArtifacts artifacts: 'outputFolder/**/*.*'
        }
    }
}
