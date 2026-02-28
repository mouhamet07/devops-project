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
            steps {
                // On utilise docker.image(...).inside si l'hôte a docker
                script {
                    docker.image('mcr.microsoft.com/dotnet/sdk:8.0').inside {
                        sh "dotnet restore gestionStock/gestionStock.csproj"
                        sh "dotnet build gestionStock/gestionStock.csproj --configuration Release"
                    }
                }
            }
        }
        stage('Docker Build & Push') {
            steps {
                script {
                    def customImage = docker.build("${DOCKER_IMAGE}:latest", "./gestionStock")
                    withCredentials([usernamePassword(
                        credentialsId: "${REGISTRY_CREDENTIALS}",
                        passwordVariable: 'DOCKER_PASS',
                        usernameVariable: 'DOCKER_USER'
                    )]) {
                        customImage.push()
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