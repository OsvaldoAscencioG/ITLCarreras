pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                git 'https://github.com/OsvaldoAscencioG/ITLCarreras'
            }
        }
        stage('Build') {
            steps {
                script {
                    dotnetBuild sdkVersion: '6.0', projects: 'CarrerasITL.csproj'
                }
            }
        }
        stage('Docker Login') {
            steps {
                bat 'docker login -u osvaldogallegos -p Communion:1'
            }
        }
        stage('Docker Build and Publish') {
            steps {
                docker.build('osvaldogallegos', 'jenkins')
                docker.withRegistry('https://index.docker.io/v1/', 'credentials-id') {
                    docker.image('osvaldogallegos').push('latest')
                }
            }
        }
    }
}
