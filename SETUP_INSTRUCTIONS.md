# Matrimony API - Setup Instructions

## Quick Start Guide

Follow these steps to get the Matrimony API up and running on your local machine.

### Step 1: Prerequisites

Make sure you have the following installed:
- **.NET 8.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** or **SQL Server Express** - [Download here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **Visual Studio 2022** (recommended) or **VS Code** with C# extension

### Step 2: Clone and Navigate

Open your terminal or command prompt:
```bash
cd MatrimonyAPI
```

### Step 3: Configure Database Connection

1. Open `appsettings.json` in the MatrimonyAPI folder
2. Update the connection string to match your SQL Server instance:

**For SQL Server Express:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=MatrimonyDB;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

**For LocalDB (Visual Studio):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MatrimonyDB;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

**For Full SQL Server:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=MatrimonyDB;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

### Step 4: Install Dependencies

```bash
dotnet restore
```

### Step 5: Create Database

Run the following commands to create and set up the database:

```bash
# Install/Update Entity Framework tools (if not already installed)
dotnet tool install --global dotnet-ef

# Create the initial migration
dotnet ef migrations add InitialCreate

# Create the database and apply migrations
dotnet ef database update
```

### Step 6: Run the Application

```bash
dotnet run
```

The API will start and be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### Step 7: Access Swagger UI

Open your browser and navigate to:
```
https://localhost:5001/swagger
```

You'll see the interactive API documentation where you can test all endpoints.

## Testing the API

### 1. Register a User

Use the `/api/auth/register` endpoint with:
```json
{
  "email": "test@example.com",
  "password": "Test@123",
  "phoneNumber": "+1234567890"
}
```

### 2. Login

Use the `/api/auth/login` endpoint with your credentials. Copy the token from the response.

### 3. Authorize in Swagger

Click the "Authorize" button in Swagger UI and enter:
```
Bearer YOUR_TOKEN_HERE
```

### 4. Create Profile

Now you can create a profile and use all other endpoints!

## Common Issues and Solutions

### Issue 1: Database Connection Failed
**Error:** Cannot open database "MatrimonyDB"

**Solution:** 
- Verify SQL Server is running
- Check your connection string matches your SQL Server instance
- Try running `sqlcmd -S localhost -E` to test connectivity

### Issue 2: Migration Command Not Found
**Error:** 'dotnet-ef' is not recognized

**Solution:**
```bash
dotnet tool install --global dotnet-ef
# If already installed, update it:
dotnet tool update --global dotnet-ef
```

### Issue 3: Port Already in Use
**Error:** Unable to bind to https://localhost:5001

**Solution:** Change the port in `Properties/launchSettings.json` or kill the process using the port.

### Issue 4: JWT Authentication Not Working
**Error:** 401 Unauthorized

**Solution:**
- Make sure you're logged in and have a valid token
- Check that you've added "Bearer " before the token in the Authorization header
- Verify the token hasn't expired (tokens expire after 7 days)

## Project Structure

```
MatrimonyAPI/
├── Controllers/           # API endpoints
│   ├── AuthController.cs
│   ├── ProfileController.cs
│   ├── PreferenceController.cs
│   ├── SearchController.cs
│   ├── InterestController.cs
│   └── MessageController.cs
├── Data/                  # Database context
│   └── MatrimonyDbContext.cs
├── DTOs/                  # Data transfer objects
├── Models/                # Database models
├── Services/              # Business logic
│   ├── AuthService.cs
│   ├── ProfileService.cs
│   ├── PreferenceService.cs
│   ├── SearchService.cs
│   ├── InterestService.cs
│   └── MessageService.cs
├── Properties/
│   └── launchSettings.json
├── appsettings.json       # Configuration
├── Program.cs             # Application entry point
└── MatrimonyAPI.csproj    # Project file
```

## Development Workflow

### Adding New Features

1. Create/Update models in `Models/`
2. Create DTOs in `DTOs/`
3. Create service interface and implementation in `Services/`
4. Create controller in `Controllers/`
5. Register service in `Program.cs`
6. Create migration and update database

### Database Changes

```bash
# Add a migration after model changes
dotnet ef migrations add MigrationName

# Apply migrations to database
dotnet ef database update

# Rollback last migration
dotnet ef migrations remove
```

## API Features

✅ User Registration & Authentication (JWT)  
✅ Profile Management with Photos  
✅ Partner Preferences  
✅ Advanced Search with Filters  
✅ Interest/Connection Management  
✅ Private Messaging between Connected Users  
✅ Pagination Support  
✅ Swagger Documentation  

## Next Steps

After getting the API running, you can:
1. Build a frontend application (React, Angular, Vue, etc.)
2. Add email verification functionality
3. Implement file upload for photos (currently using URLs)
4. Add admin dashboard endpoints
5. Implement real-time chat using SignalR
6. Add payment/subscription features
7. Deploy to cloud (Azure, AWS, etc.)

## Support

For issues or questions:
1. Check the README.md for detailed documentation
2. Review the Swagger documentation at `/swagger`
3. Check the error logs in the console

## Environment Variables

You can also configure settings via environment variables:

```bash
# Windows PowerShell
$env:ConnectionStrings__DefaultConnection = "Your connection string"
$env:JwtSettings__SecretKey = "Your secret key"

# Windows Command Prompt
set ConnectionStrings__DefaultConnection=Your connection string
set JwtSettings__SecretKey=Your secret key

# Linux/Mac
export ConnectionStrings__DefaultConnection="Your connection string"
export JwtSettings__SecretKey="Your secret key"
```

Happy Coding! 🚀

