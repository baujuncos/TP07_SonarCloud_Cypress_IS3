# TikTask2.0

TikTask2.0 is a modern task management web application built with .NET 9, featuring a Blazor Server frontend and ASP.NET Core Web API backend with SQLite database.

## Features

- **User Authentication**: Register and login with secure password hashing (BCrypt)
- **Task Management**: Create, edit, delete, and mark tasks as complete
- **Due Dates**: Set due dates for tasks to stay organized
- **Admin Role**: Admin users can view all users' tasks
- **Responsive Design**: Clean and modern UI built with Blazor
- **JWT Authentication**: Secure API endpoints with JWT tokens
- **SQLite Database**: Lightweight and portable database

## Project Structure

```
TikTask2.0/
├── src/
│   ├── TikTask2.0.API/          # Backend Web API
│   │   ├── Controllers/         # API Controllers
│   │   ├── Data/               # Database Context
│   │   ├── DTOs/               # Data Transfer Objects
│   │   ├── Models/             # Database Models
│   │   └── Services/           # Business Services
│   └── TikTask2.0.Web/          # Frontend Blazor App
│       ├── Components/          # Blazor Components
│       ├── Models/             # Client Models
│       └── Services/           # API Client Services
├── azure-pipelines.yml          # Azure DevOps Pipeline
└── TikTask2.0.sln              # Solution File
```

## Technologies Used

- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API backend
- **Blazor Server**: Interactive web UI
- **Entity Framework Core**: ORM for database access
- **SQLite**: Lightweight database
- **JWT (JSON Web Tokens)**: Authentication
- **BCrypt**: Password hashing

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A code editor (Visual Studio, VS Code, or Rider)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/baujuncos/TP5_IS3.git
cd TP5_IS3
```

### 2. Run the API

```bash
cd src/TikTask2.0.API
dotnet run
```

The API will start on `https://localhost:7001`

### 3. Run the Web Application

In a new terminal:

```bash
cd src/TikTask2.0.Web
dotnet run
```

The web app will start on `https://localhost:7002`

### 4. Access the Application

Open your browser and navigate to `https://localhost:7002`

## Default Users

On first run, you can register a new user. To create an admin user, you'll need to manually update the database or modify the registration code.

### Creating an Admin User

After registering a regular user, you can modify the database directly:

1. Stop the API
2. Open the `tiktask.db` file with a SQLite tool
3. Update the user's Role to "Admin":
   ```sql
   UPDATE Users SET Role = 'Admin' WHERE Username = 'yourusername';
   ```
4. Restart the API

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token

### Tasks
- `GET /api/tasks` - Get current user's tasks
- `GET /api/tasks/all` - Get all tasks (Admin only)
- `GET /api/tasks/{id}` - Get specific task
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update task
- `PATCH /api/tasks/{id}/complete` - Toggle task completion
- `DELETE /api/tasks/{id}` - Delete task

## Azure DevOps Deployment

This project includes an `azure-pipelines.yml` file for automated deployment to Azure.

### Setup Steps:

1. **Create Azure Resources**:
   - Two Azure App Services (one for API, one for Web)
   - Configure connection strings in App Service settings

2. **Configure Pipeline Variables**:
   - `AzureSubscription`: Your Azure service connection
   - `ApiAppName`: Name of the API App Service
   - `WebAppName`: Name of the Web App Service

3. **Update Configuration**:
   - Update `appsettings.json` in Web project with production API URL
   - Update JWT settings for production use

4. **Create Pipeline**:
   - In Azure DevOps, create a new pipeline
   - Select the repository and use the existing `azure-pipelines.yml`

## Configuration

### API (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=tiktask.db"
  },
  "Jwt": {
    "Key": "your-secret-key-here",
    "Issuer": "TikTask2.0",
    "Audience": "TikTask2.0Users"
  }
}
```

### Web (appsettings.json)

```json
{
  "ApiUrl": "https://localhost:7001"
}
```

## Security Notes

⚠️ **Important for Production**:
- Change the JWT secret key to a strong, randomly generated value
- Use HTTPS in production
- Store connection strings and secrets in Azure Key Vault or environment variables
- Enable CORS only for specific origins, not all origins
- Consider adding rate limiting for API endpoints

## Development

### Building the Solution

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.

## Support

For issues and questions, please open an issue on GitHub.
