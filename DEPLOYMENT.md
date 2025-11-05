# Deployment Guide for TikTask2.0

This guide covers different deployment options for TikTask2.0 application.

## Table of Contents
1. [Azure App Service Deployment](#azure-app-service-deployment)
2. [Docker Deployment](#docker-deployment)
3. [Azure DevOps CI/CD](#azure-devops-cicd)
4. [Local Development](#local-development)

## Azure App Service Deployment

### Prerequisites
- Azure Subscription
- Azure CLI installed

### Step 1: Create Azure Resources

```bash
# Login to Azure
az login

# Create resource group
az group create --name TikTask-RG --location eastus

# Create App Service Plan
az appservice plan create --name TikTask-Plan --resource-group TikTask-RG --sku B1 --is-linux

# Create App Service for API
az webapp create --name tiktask-api --resource-group TikTask-RG --plan TikTask-Plan --runtime "DOTNETCORE:9.0"

# Create App Service for Web
az webapp create --name tiktask-web --resource-group TikTask-RG --plan TikTask-Plan --runtime "DOTNETCORE:9.0"
```

### Step 2: Configure App Services

#### API Configuration
```bash
# Set environment variables
az webapp config appsettings set --name tiktask-api --resource-group TikTask-RG --settings \
  ConnectionStrings__DefaultConnection="Data Source=/home/data/tiktask.db" \
  Jwt__Key="YOUR_SECURE_JWT_KEY_HERE_MINIMUM_32_CHARACTERS" \
  Jwt__Issuer="TikTask2.0" \
  Jwt__Audience="TikTask2.0Users"
```

#### Web Configuration
```bash
# Set API URL
az webapp config appsettings set --name tiktask-web --resource-group TikTask-RG --settings \
  ApiUrl="https://tiktask-api.azurewebsites.net"
```

### Step 3: Deploy Applications

```bash
# Build and publish API
cd src/TikTask2.0.API
dotnet publish -c Release -o ./publish
cd publish
zip -r api.zip .
az webapp deployment source config-zip --resource-group TikTask-RG --name tiktask-api --src api.zip

# Build and publish Web
cd ../../TikTask2.0.Web
dotnet publish -c Release -o ./publish
cd publish
zip -r web.zip .
az webapp deployment source config-zip --resource-group TikTask-RG --name tiktask-web --src web.zip
```

### Step 4: Verify Deployment

1. Navigate to `https://tiktask-api.azurewebsites.net/api/`
2. Navigate to `https://tiktask-web.azurewebsites.net`

## Docker Deployment

### Prerequisites
- Docker installed
- Docker Compose installed

### Option 1: Using Docker Compose

```bash
# Build and run all services
docker-compose up -d

# Check status
docker-compose ps

# View logs
docker-compose logs -f

# Stop services
docker-compose down
```

Access:
- API: http://localhost:5001
- Web: http://localhost:5002

### Option 2: Build Individual Containers

#### Build API Container
```bash
cd src/TikTask2.0.API
docker build -t tiktask-api -f Dockerfile ../..
docker run -d -p 5001:8080 --name tiktask-api tiktask-api
```

#### Build Web Container
```bash
cd src/TikTask2.0.Web
docker build -t tiktask-web -f Dockerfile ../..
docker run -d -p 5002:8080 --name tiktask-web \
  -e ApiUrl=http://tiktask-api:8080 \
  --link tiktask-api \
  tiktask-web
```

## Azure DevOps CI/CD

### Setup Instructions

1. **Create Azure DevOps Project**
   - Go to https://dev.azure.com
   - Create a new project

2. **Connect to GitHub Repository**
   - Go to Pipelines > Create Pipeline
   - Select GitHub
   - Select the repository: baujuncos/TP5_IS3
   - Use existing Azure Pipelines YAML file
   - Select `azure-pipelines.yml`

3. **Configure Pipeline Variables**
   - Go to Pipelines > Library > Variable Groups
   - Create a variable group named "TikTask-Variables"
   - Add the following variables:
     - `AzureSubscription`: Your Azure service connection name
     - `ApiAppName`: Name of your API App Service (e.g., tiktask-api)
     - `WebAppName`: Name of your Web App Service (e.g., tiktask-web)

4. **Create Azure Service Connection**
   - Go to Project Settings > Service Connections
   - Create new Azure Resource Manager connection
   - Select your subscription and resource group
   - Name it (use this name for `AzureSubscription` variable)

5. **Run Pipeline**
   - Go to Pipelines
   - Run the pipeline
   - Monitor the build and deployment

### Pipeline Stages

The pipeline includes:
1. **Build Stage**: Restores, builds, and publishes both API and Web projects
2. **Deploy Stage**: Deploys to Azure App Services (runs only on main branch)

## Local Development

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 / VS Code / Rider

### Running Locally

#### Option 1: Using Command Line

Terminal 1 - API:
```bash
cd src/TikTask2.0.API
dotnet run --launch-profile http
```

Terminal 2 - Web:
```bash
cd src/TikTask2.0.Web
dotnet run --launch-profile http
```

Access:
- API: http://localhost:5001
- Web: http://localhost:5002
- Default Admin: username=admin, password=Admin123!

#### Option 2: Using Visual Studio

1. Right-click on the solution
2. Select "Configure Startup Projects"
3. Choose "Multiple startup projects"
4. Set both TikTask2.0.API and TikTask2.0.Web to "Start"
5. Press F5 to run

### Database

The application uses SQLite and creates a database file at:
- `src/TikTask2.0.API/tiktask.db`

To reset the database:
```bash
cd src/TikTask2.0.API
rm tiktask.db
dotnet run
```

## Environment Variables

### API
| Variable | Description | Default |
|----------|-------------|---------|
| `ConnectionStrings__DefaultConnection` | Database connection string | `Data Source=tiktask.db` |
| `Jwt__Key` | JWT signing key (min 32 chars) | See appsettings.json |
| `Jwt__Issuer` | JWT issuer | `TikTask2.0` |
| `Jwt__Audience` | JWT audience | `TikTask2.0Users` |

### Web
| Variable | Description | Default |
|----------|-------------|---------|
| `ApiUrl` | Backend API URL | `http://localhost:5001` |

## Security Considerations

### For Production Deployment:

1. **Change JWT Secret Key**
   ```bash
   # Generate a secure random key
   openssl rand -base64 64
   ```

2. **Use Azure Key Vault**
   - Store connection strings and JWT keys in Azure Key Vault
   - Update App Service to use Key Vault references

3. **Enable HTTPS**
   - Ensure HTTPS is enabled in production
   - Update CORS policy to allow only specific origins

4. **Database**
   - For production, consider using Azure SQL Database instead of SQLite
   - Enable automatic backups

5. **Monitoring**
   - Enable Application Insights
   - Configure alerts for errors and performance

## Troubleshooting

### API Not Starting
- Check if port 5001 is available
- Verify .NET 9.0 SDK is installed
- Check application logs

### Web Cannot Connect to API
- Verify API is running
- Check `ApiUrl` configuration in Web appsettings
- Check CORS configuration in API

### Database Issues
- Delete the database file and restart to recreate
- Check file permissions on database file
- Ensure SQLite is supported on hosting platform

### Azure Deployment Issues
- Check App Service logs in Azure Portal
- Verify environment variables are set correctly
- Ensure .NET 9.0 runtime is available

## Support

For issues and questions, please open an issue on the GitHub repository.
