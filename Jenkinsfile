pipeline {
    agent any  

    environment {
        DOCKER_IMAGE = "mouhamet07/devops-project"
        REGISTRY_CREDENTIALS = "dockerhub-cred"
        DOCKER_TAG = "${BUILD_NUMBER}" 
        DOTNET_CLI_HOME = "${WORKSPACE}/.dotnet"
        K8S_DEPLOYMENT = "gestionstock-deployment"
        K8S_CONTAINER = "gestionstock-container"
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
                sh "docker build -t ${DOCKER_IMAGE}:${DOCKER_TAG} ."
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
            steps {
                withCredentials([file(credentialsId: 'kubeconf', variable: 'KUBECONFIG_PATH')]) {
                    
                    script {
                        // Vérifie si le deployment existe
                        def exists = sh(
                            script: "kubectl get deployment ${K8S_DEPLOYMENT} --kubeconfig $KUBECONFIG_PATH --ignore-not-found",
                            returnStatus: true
                        ) == 0
                        
                        if (exists) {
                            sh "kubectl set image deployment/${K8S_DEPLOYMENT} ${K8S_CONTAINER}=${DOCKER_IMAGE}:${DOCKER_TAG} --kubeconfig $KUBECONFIG_PATH"
                        } else {
                            sh "kubectl apply -f k8s/deployment.yaml --kubeconfig $KUBECONFIG_PATH"
                            sh "kubectl set image deployment/${K8S_DEPLOYMENT} ${K8S_CONTAINER}=${DOCKER_IMAGE}:${DOCKER_TAG} --kubeconfig $KUBECONFIG_PATH"
                        }
                        sh "kubectl rollout status deployment/${K8S_DEPLOYMENT} --kubeconfig $KUBECONFIG_PATH"
                    }
                }
            }
        }

    }
}