def call(String projectPath, String buildType) {
    script {
    	sh '''
            rm -rf outputFolder
            mkdir outputFolder
            
            echo "Unity Build starting..."
            /Applications/Unity/Hub/Editor/2020.3.20f1/Unity.app/Contents/MacOS/Unity -quit -batchmode -projectPath ${projectPath} -executeMethod "ExportTool.ExportXcodeProject" -buildType ${buildType} -logFile ./outputFolder/export.log
            echo "Unity Build finished..."
        '''
    }
}