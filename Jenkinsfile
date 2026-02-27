pipeline {
    agent any 
    environment {
        DOCKER_IMAGE = "mouhamet07/devops-project"
        REGISTRY_CREDENTIALS = "devops"
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
                }
            }
            steps {
                sh "dotnet restore gestionStock/gestionStock.csproj"
                sh "dotnet build gestionStock/gestionStock.csproj --configuration Release"
            }
        }
        stage('Docker Build & Push') {
            steps {
                script {
                    sh "docker build -t ${DOCKER_IMAGE}:latest ./gestionStock"
                    withCredentials([usernamePassword(
                        credentialsId: "${REGISTRY_CREDENTIALS}",
                        passwordVariable: 'DOCKER_PASS',
                        usernameVariable: 'DOCKER_USER'
                    )]) {
                        sh "echo ${DOCKER_PASS} | docker login -u ${DOCKER_USER} --password-stdin"
                        sh "docker push ${DOCKER_IMAGE}:latest"
                    }
                }
            }
        }
        stage('Deploy to Kubernetes') {
            steps {
                sh "kubectl apply -f gestionStock/k8s/deployment.yaml"
                sh "kubectl apply -f gestionStock/k8s/service.yaml"
            }
        }
    }
}