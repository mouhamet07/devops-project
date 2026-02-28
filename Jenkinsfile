pipeline {
    agent any   // ← default = your Jenkins container (has docker)

    environment {
        DOCKER_IMAGE = "mouhamet07/devops-project"
        REGISTRY_CREDENTIALS = "devops"
        DOCKER_TAG = "latest"   // or "${BUILD_NUMBER}" later
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
                    // Reuse workspace so you don't re-clone git inside container
                    reuseNode true
                }
            }
            steps {
                dir('gestionStock') {
                    sh 'dotnet restore gestionStock.csproj'
                    sh 'dotnet build gestionStock.csproj --configuration Release'
                    // sh 'dotnet test ...' if you have tests
                }
            }
        }

        stage('Docker Build & Push') {
            // ← back to Jenkins container → docker works here
            steps {
                dir('gestionStock') {   // or wherever your Dockerfile lives
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