# 💍 Matrimony API

A comprehensive RESTful API for a matrimony/matchmaking platform built with **ASP.NET Core 8.0** and **MongoDB Atlas**.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-Atlas-47A248?logo=mongodb)](https://www.mongodb.com/atlas)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## ✨ Features

- 🔐 **JWT Authentication** - Secure user registration and login
- 👤 **Profile Management** - Detailed user profiles with photos
- 🎯 **Partner Preferences** - Define search criteria for matches
- 🔍 **Advanced Search** - Filter by age, religion, location, education, etc.
- ❤️ **Interest System** - Send/receive/accept connection requests
- 💬 **Private Messaging** - Chat with connected users
- 📸 **Photo Management** - Upload and manage multiple photos
- 🌐 **Cloud-Ready** - MongoDB Atlas integration
- 📊 **Automatic Indexing** - Optimized database queries
- 📖 **Swagger Documentation** - Interactive API documentation

## 🚀 Quick Start

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- MongoDB Atlas account (free tier available)

### Installation

```bash
# Clone the repository
git clone https://github.com/YOUR_USERNAME/matrimony.git
cd matrimony

# Navigate to API project
cd MatrimonyAPI

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

### Access the API

- **Swagger UI**: https://localhost:5001/swagger
- **API Base URL**: https://localhost:5001/api

## 📖 API Documentation

### Authentication

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/auth/register` | POST | Register new user |
| `/api/auth/login` | POST | Login user |
| `/api/auth/change-password` | POST | Change password |
| `/api/auth/me` | GET | Get current user |

### Profiles

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/profile` | POST | Create profile |
| `/api/profile/me` | GET | Get own profile |
| `/api/profile/{id}` | GET | Get profile by ID |
| `/api/profile` | PUT | Update profile |
| `/api/profile/photos` | POST | Upload photo |

### Search & Matching

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/search` | POST | Search profiles with filters |
| `/api/search/matches` | GET | Get matches based on preferences |
| `/api/preference` | POST | Set partner preferences |

### Interests (Connections)

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/interest` | POST | Send interest |
| `/api/interest/respond` | POST | Accept/reject interest |
| `/api/interest/sent` | GET | View sent interests |
| `/api/interest/received` | GET | View received interests |

### Messaging

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/message` | POST | Send message |
| `/api/message/conversations` | GET | Get all conversations |
| `/api/message/conversation/{userId}` | GET | Get conversation with user |
| `/api/message/unread-count` | GET | Get unread message count |

📄 **Full API Documentation**: See [API_ENDPOINTS.md](API_ENDPOINTS.md)

## 🗄️ Database Structure

### Collections

- **users** - User accounts and authentication
- **profiles** - Detailed user profiles
- **partnerPreferences** - Partner search criteria
- **photos** - User photos
- **interests** - Connection requests
- **messages** - Private messages

### Indexes

Automatic indexes created for optimal performance:
- Unique index on user email
- Profile search index (gender, religion, city)
- User ID indexes on all related collections

## ⚙️ Configuration

Update `MatrimonyAPI/appsettings.json`:

```json
{
  "MongoDB": {
    "ConnectionString": "your-mongodb-connection-string",
    "DatabaseName": "MatrimonyDB"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-min-32-characters",
    "Issuer": "MatrimonyAPI",
    "Audience": "MatrimonyClient"
  }
}
```

**Production**: Use environment variables for sensitive data.

## 🧪 Testing

### Using Swagger UI

1. Navigate to https://localhost:5001/swagger
2. Click **POST /api/auth/register** to create an account
3. Click **POST /api/auth/login** to get a token
4. Click **Authorize** button and enter: `Bearer YOUR_TOKEN`
5. Test other endpoints!

### Using cURL

```bash
# Register
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"Test@123","phoneNumber":"+1234567890"}'

# Login
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"Test@123"}'
```

## 📁 Project Structure

```
MatrimonyAPI/
├── Controllers/           # API endpoints
│   ├── AuthController.cs
│   ├── ProfileController.cs
│   ├── SearchController.cs
│   ├── InterestController.cs
│   └── MessageController.cs
├── Services/             # Business logic
│   ├── AuthService.cs
│   ├── ProfileService.cs
│   ├── SearchService.cs
│   ├── InterestService.cs
│   └── MessageService.cs
├── Models/               # MongoDB documents
│   ├── User.cs
│   ├── Profile.cs
│   ├── Interest.cs
│   └── Message.cs
├── DTOs/                 # Data transfer objects
├── Data/                 # Database context
└── Program.cs            # Application configuration
```

## 🔒 Security Features

- ✅ JWT token-based authentication
- ✅ BCrypt password hashing
- ✅ Role-based authorization
- ✅ Token expiration (7 days)
- ✅ CORS configuration
- ✅ HTTPS enforcement

## 🌐 MongoDB Atlas Setup

1. Create free account at [MongoDB Atlas](https://www.mongodb.com/atlas)
2. Create a new cluster
3. Add database user
4. Configure network access (add your IP)
5. Get connection string
6. Update `appsettings.json`

**Detailed guide**: See [MONGODB_SETUP.md](MONGODB_SETUP.md)

## 🚢 Deployment

### Azure App Service

```bash
# Publish the application
dotnet publish -c Release -o ./publish

# Deploy to Azure (configure App Service first)
az webapp deployment source config-zip \
  --resource-group YourResourceGroup \
  --name YourAppName \
  --src publish.zip
```

### Docker

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY publish/ .
EXPOSE 80
ENTRYPOINT ["dotnet", "MatrimonyAPI.dll"]
```

## 📊 MongoDB vs SQL Server

| Feature | MongoDB | SQL Server |
|---------|---------|------------|
| Setup | Automatic | Migrations required |
| Schema | Flexible | Fixed |
| Scalability | Horizontal | Vertical |
| Cloud | Atlas included | Self-hosted |
| IDs | ObjectId (strings) | Sequential integers |

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📚 Additional Documentation

- 📖 [API Endpoints Reference](API_ENDPOINTS.md)
- 🚀 [Quick Start Guide](QUICK_START.md)
- 🔧 [Setup Instructions](SETUP_INSTRUCTIONS.md)
- 🗄️ [MongoDB Setup](MONGODB_SETUP.md)
- 📋 [Migration Summary](MIGRATION_SUMMARY.md)
- ⭐ [Getting Started](START_HERE.md)

## 🛠️ Built With

- [ASP.NET Core 8.0](https://dotnet.microsoft.com/) - Web framework
- [MongoDB.Driver](https://mongodb.github.io/mongo-csharp-driver/) - Database driver
- [JWT Bearer](https://jwt.io/) - Authentication
- [BCrypt.Net](https://github.com/BcryptNet/bcrypt.net) - Password hashing
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - API documentation

## 🎯 Roadmap

- [ ] Email verification
- [ ] Phone OTP verification
- [ ] Real-time chat with SignalR
- [ ] File upload for photos
- [ ] Payment integration
- [ ] Admin dashboard
- [ ] Advanced matching algorithm
- [ ] Mobile app (React Native/Flutter)


## 🙏 Acknowledgments

- MongoDB Atlas for free cloud database hosting
- Microsoft for excellent .NET documentation
- The open-source community
- Cusor and copilot

---

⭐ **Star this repo** if you find it helpful!
