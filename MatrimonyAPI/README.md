# Matrimony API

A comprehensive backend API for a matrimony site built with ASP.NET Core 8.0.

## Features

- **User Management**
  - User registration and login with JWT authentication
  - Password management
  - Email and phone verification support

- **Profile Management**
  - Create and update detailed user profiles
  - Photo upload and management
  - Profile visibility controls

- **Partner Preferences**
  - Define partner preferences (age, height, religion, location, etc.)
  - Match profiles based on preferences

- **Search & Matching**
  - Advanced search with multiple filters
  - Get matches based on partner preferences
  - Pagination support

- **Interest Management**
  - Send interest to other profiles
  - Accept or reject received interests
  - View sent and received interests
  - Cancel pending interests

- **Messaging**
  - Send messages to connected users (after interest acceptance)
  - View conversations
  - Mark messages as read
  - Unread message count

## Technology Stack

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server
- JWT Authentication
- BCrypt for password hashing
- Swagger for API documentation

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server or SQL Server Express
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
```bash
git clone <repository-url>
cd MatrimonyAPI
```

2. Update the database connection string in `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MatrimonyDB;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

3. Run database migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

5. Access Swagger UI at: `https://localhost:5001/swagger`

## API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user
- `POST /api/auth/change-password` - Change password
- `GET /api/auth/me` - Get current user info

### Profile
- `POST /api/profile` - Create profile
- `GET /api/profile/me` - Get own profile
- `GET /api/profile/{profileId}` - Get profile by ID
- `PUT /api/profile` - Update profile
- `DELETE /api/profile` - Delete profile
- `POST /api/profile/photos` - Upload photo
- `DELETE /api/profile/photos/{photoId}` - Delete photo
- `PUT /api/profile/photos/{photoId}/set-primary` - Set primary photo

### Preferences
- `POST /api/preference` - Create or update preferences
- `GET /api/preference` - Get preferences
- `DELETE /api/preference` - Delete preferences

### Search
- `POST /api/search` - Search profiles with filters
- `GET /api/search/matches` - Get matches based on preferences

### Interest
- `POST /api/interest` - Send interest
- `POST /api/interest/respond` - Respond to interest
- `GET /api/interest/sent` - Get sent interests
- `GET /api/interest/received` - Get received interests
- `DELETE /api/interest/{interestId}` - Cancel interest

### Message
- `POST /api/message` - Send message
- `GET /api/message/conversations` - Get all conversations
- `GET /api/message/conversation/{otherUserId}` - Get conversation with user
- `PUT /api/message/{messageId}/read` - Mark message as read
- `GET /api/message/unread-count` - Get unread message count

## Database Schema

### Users
- User authentication and basic info
- Email, password hash, phone number
- Account status and verification flags

### Profiles
- Detailed user profile information
- Personal details, education, occupation
- Location information

### PartnerPreferences
- Partner search criteria
- Age range, height range, religion, etc.

### Photos
- User photos with approval status
- Primary photo designation

### Interests
- Connection requests between users
- Status tracking (pending, accepted, rejected, cancelled)

### Messages
- Private messaging between connected users
- Read status tracking

## Security

- JWT token-based authentication
- BCrypt password hashing
- Authorization on protected endpoints
- CORS configuration for cross-origin requests

## Configuration

Update `appsettings.json` for:
- Database connection string
- JWT secret key and settings
- Logging levels

## Development

### Entity Framework Migrations

```bash
# Add a new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Running Tests

```bash
dotnet test
```

## Production Deployment

1. Update `appsettings.json` with production values
2. Set a strong JWT secret key
3. Configure production database connection
4. Enable HTTPS
5. Configure proper CORS policies
6. Set up logging and monitoring

## License

[Your License]

## Contributing

[Your Contributing Guidelines]

