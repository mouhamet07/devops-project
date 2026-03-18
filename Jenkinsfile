pipeline {
    agent any  

    environment {
        DOCKER_IMAGE = "mouhamet07/devops-project"
        REGISTRY_CREDENTIALS = "devops"
        DOCKER_TAG = "${BUILD_NUMBER}" 
        DOTNET_CLI_HOME = "${WORKSPACE}/.dotnet"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build & Test') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:8.0'
                    reuseNode true
                }
            }
            steps {
                dir('gestionStock/gestionStock') {
                    sh 'dotnet restore gestionStock.csproj'
                    sh 'dotnet build gestionStock.csproj --configuration Release'
                }
            }
        }

        stage('Docker Build & Push') {
            steps {
                dir('gestionStock') {   
                    sh "docker build -t ${DOCKER_IMAGE}:${DOCKER_TAG} ."
                }

                withCredentials([usernamePassword(
                    credentialsId: "${REGISTRY_CREDENTIALS}",
                    usernameVariable: 'USER',
                    passwordVariable: 'PASS'
                )]) {
                    sh 'echo $PASS | docker login -u $USER --password-stdin'
                    sh "docker push ${DOCKER_IMAGE}:${DOCKER_TAG}"
                }
            }
        }

        stage('Deploy to Kubernetes') {
            // ← also on Jenkins container → needs kubectl installed
            steps {
                sh '''
                    kubectl apply -f gestionStock/k8s/deployment.yaml
                    kubectl apply -f gestionStock/k8s/service.yaml
                '''
            }
        }
    }
}