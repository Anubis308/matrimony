# 🚀 Quick Start - Matrimony API

## ⚡ Get Running in 5 Minutes

### 1. Install Prerequisites (if not already installed)
```bash
# Download .NET 8 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0
# SQL Server Express: https://www.microsoft.com/sql-server/sql-server-downloads
```

### 2. Navigate to Project
```bash
cd MatrimonyAPI
```

### 3. Restore Packages
```bash
dotnet restore
```

### 4. Update Connection String
Edit `appsettings.json` - update the connection string for your SQL Server:
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MatrimonyDB;Trusted_Connection=true;TrustServerCertificate=true;"
```

### 5. Create Database
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 6. Run the API
```bash
dotnet run
```

### 7. Open Swagger
Browser → `https://localhost:5001/swagger`

---

## 🎯 First API Calls

### Test in Swagger UI:

**1. Register User**
```
POST /api/auth/register
```
Body:
```json
{
  "email": "john@example.com",
  "password": "Test@123",
  "phoneNumber": "+1234567890"
}
```

**2. Copy the token from response**

**3. Click "Authorize" button, paste:**
```
Bearer YOUR_TOKEN_HERE
```

**4. Create Profile**
```
POST /api/profile
```
Body:
```json
{
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "1990-01-15",
  "gender": 0,
  "religion": "Christian",
  "community": "Catholic",
  "motherTongue": "English",
  "maritalStatus": 0,
  "heightInCm": 175,
  "education": "Bachelor's",
  "occupation": "Engineer",
  "country": "USA",
  "state": "California",
  "city": "San Francisco",
  "about": "Software engineer passionate about technology",
  "familyDetails": "Close-knit family",
  "hobbies": "Reading, traveling, coding"
}
```

**5. Search Profiles**
```
POST /api/search
```

---

## 📁 Project Structure

```
Matrimony/
├── MatrimonyAPI/              # Main API Project
│   ├── Controllers/           # API Endpoints
│   ├── Services/              # Business Logic
│   ├── Models/                # Database Models
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Data/                  # Database Context
│   ├── Program.cs             # App Configuration
│   └── appsettings.json       # Settings
├── Matrimony.sln              # Solution File
├── README.md                  # Full Documentation
├── SETUP_INSTRUCTIONS.md      # Detailed Setup Guide
├── API_ENDPOINTS.md           # Complete API Reference
└── QUICK_START.md            # This File
```

---

## 🎨 Features Included

✅ **Authentication**
- JWT Token-based auth
- Register & Login
- Password management

✅ **Profile Management**
- Complete user profiles
- Photo upload/management
- Profile updates

✅ **Partner Preferences**
- Define search criteria
- Age, height, religion filters
- Location preferences

✅ **Search & Matching**
- Advanced search
- Match suggestions
- Pagination support

✅ **Interest System**
- Send/receive interests
- Accept/reject interests
- Connection management

✅ **Messaging**
- Private messaging
- Conversation threads
- Read receipts
- Unread count

---

## 🔧 Common Commands

```bash
# Run the application
dotnet run

# Build the project
dotnet build

# Run with watch (auto-reload)
dotnet watch run

# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Restore packages
dotnet restore

# Clean build artifacts
dotnet clean
```

---

## 🐛 Troubleshooting

**Port already in use?**
```bash
# Change port in Properties/launchSettings.json
```

**Database connection failed?**
```bash
# Check SQL Server is running
# Update connection string in appsettings.json
```

**Migration command not found?**
```bash
dotnet tool install --global dotnet-ef
```

**401 Unauthorized?**
```bash
# Make sure to:
# 1. Login and get token
# 2. Click Authorize in Swagger
# 3. Enter: Bearer YOUR_TOKEN
```

---

## 📚 Documentation Files

- **README.md** - Complete project documentation
- **SETUP_INSTRUCTIONS.md** - Detailed setup guide with troubleshooting
- **API_ENDPOINTS.md** - Full API reference with examples
- **QUICK_START.md** - This file (fastest way to get started)

---

## 🎓 Learning Path

1. **Day 1:** Get it running, test auth endpoints
2. **Day 2:** Create profiles, test search
3. **Day 3:** Test interest and messaging system
4. **Day 4:** Build a frontend or mobile app
5. **Day 5:** Add custom features

---

## 🌟 Next Steps

- [ ] Build a frontend (React, Angular, Vue)
- [ ] Add real file upload for photos
- [ ] Implement email verification
- [ ] Add SignalR for real-time chat
- [ ] Create admin dashboard
- [ ] Add payment integration
- [ ] Deploy to cloud (Azure/AWS)

---

## 💬 Need Help?

Check the documentation:
1. Full details → `README.md`
2. Setup issues → `SETUP_INSTRUCTIONS.md`
3. API usage → `API_ENDPOINTS.md`
4. Swagger UI → `https://localhost:5001/swagger`

---

**Happy Coding! 🎉**

