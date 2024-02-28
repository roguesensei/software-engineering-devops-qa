# software-engineering-devops-qa
![Build Status](https://github.com/roguesensei/software-engineering-devops-qa/actions/workflows/docker-image.yml/badge.svg?event=push)

A simple LMS prototype build with .NET 8.0 and reactjs
## Building
### Using Docker (Recommended)
```sh
docker build -t lms:latest .
```
### Using .NET CLI
```sh
dotnet restore
cd software-engineering-devops-qa
dotnet build
dotnet run
```
