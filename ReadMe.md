# Testcontainers Example
## This is a simple example of how to use [Testcontainers](https://www.testcontainers.org/) to test an ASP.NET Core Web API.

### Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)
- IDE
  - [Visual Studio](https://visualstudio.microsoft.com/) (Windows only) 
  - [Visual Studio Code](https://code.visualstudio.com/) (Windows, macOS, Linux)
  - [JetBrains Rider](https://www.jetbrains.com/rider/) (Windows, macOS, Linux)

### Before Running App or Tests
- Start Docker

### Running the tests

#### Command Line
```bash
dotnet test
```

### Running the app

##### Spin up local DB in Docker
```bash
docker compose up -d
```

#### Then run the app
```bash
dotnet run --project src/TestcontainersExample.Web --launch-profile https
```
A browser should pop up with Swagger UI. If not, navigate to https://localhost:7225/index.html