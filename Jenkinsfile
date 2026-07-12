pipeline {
    agent any 

    environment {
        BUILD_CONFIG = 'Release'
        // Point this to your local Windows SQL Server instance
        TEST_CONN_STR = "Server=localhost;Database=EmployeeCRUD_Test;Trusted_Connection=True;TrustServerCertificate=True;"
    }

    stages {
        stage('Checkout Code') {
            steps {
                echo 'Pulling source code from GitHub...'
                checkout scm
            }
        }

        stage('Restore NuGet Packages') {
            steps {
                echo 'Restoring dependencies...'
                bat 'dotnet restore' 
            }
        }

        stage('Build Project') {
            steps {
                echo "Building application in ${env.BUILD_CONFIG} mode..."
                bat "dotnet build --configuration ${env.BUILD_CONFIG} --no-restore"
            }
        }

        stage('Execute Tests') {
            steps {
                echo 'Running Unit and Integration tests against local SQL Server...'
                // FIXED FOR WINDOWS: Enclosed commands using a multi-line string block so the variable remains active
                bat """
                    set ConnectionStrings__DefaultConnection=${env.TEST_CONN_STR}
                    dotnet test --no-build --configuration ${env.BUILD_CONFIG} --verbosity normal
                """
            }
        }

        stage('Publish Artifacts') {
            steps {
                echo 'Packaging application binaries...'
                bat "dotnet publish --configuration ${env.BUILD_CONFIG} -o %WORKSPACE%\\publish"
                
                echo 'Archiving build artifacts in Jenkins...'
                archiveArtifacts artifacts: 'publish/**', fingerprint: true
            }
        }

        stage('Deploy (Optional)') {
            when {
                branch 'main'
            }
            steps {
                echo 'Deploying to Local Windows Host...'
                // Example for deploying directly to a local IIS folder:
                // bat 'xcopy /y /s "%WORKSPACE%\\publish\\*" "C:\\inetpub\\wwwroot\\EmployeeCRUD"'
            }
        }
    }

    post {
        success {
            echo 'Pipeline finished successfully!'
        }
        failure {
            echo 'Pipeline failed. Please review console output.'
        }
    }
}