@Library(jenkins-shared-lib-example@main)

node {
    stage("iOS Unity Build") {     
        iOSUnityBuild pwd(), "Release"
    }

    stage("iOS Archive") {     
        iOSArchive ".mobileprovision", "./iOSProj/Unity-iPhone.xcodeproj", "./outputFolder/jenkins-test.xcarchive"
    }
}
