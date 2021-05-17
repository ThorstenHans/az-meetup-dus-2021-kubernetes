# Azure Meetup DUS - Kubernetes für .NET-Core-Anwendungsentwickler

This repository contains all samples from my talk about Kubernetes for .NET devs at Azure Meetup DUS.

## Slides

You can find the slides at https://speakerdeck.com/ThorstenHans/


## Contents

- The `iac` folder contains simple shell scripts to `setup` and `teardown` the demo environment
- The `src` folder contains a .NET application which consists of multiple projects
  - See `Thinktecture.Samples.WebAPI` for API application
  - See `Thinktecture.Samples.Entities` for EF Core migrations
  - See `THinktecture.Samples.Jobs.Cleanup` for a job that should be executed on a schedule
- The `root` folder contains a set of Dockerfiles to containerize the application components
- The `kubernetes` folder contains a bunch of scripts and manifests for Kubernetes deployment
  - First create required namespace using `kubectl apply -f ./kubernetes/namespaces.yml`
  - Install NGINX Ingress Controller using the `install_ingress.sh` script
  - Deploy the connection string for SQL Azure database to the cluster using `deploy_secrets.sh`
  - Deploy the API using `kubectl apply -f ./kubernetes/api`
  - Deploy the CleanUp job using `kubectl apply -f ./kubernetes/cleanup`
