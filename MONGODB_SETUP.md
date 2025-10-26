# MongoDB Matrimony API - Setup Guide

## ✅ MongoDB Configuration Complete!

Your matrimony API is now configured to use **MongoDB Atlas** instead of SQL Server.

### 📝 What Changed

1. **Database**: MongoDB Atlas (Cloud-hosted)
2. **Connection String**: Already configured in `appsettings.json`
3. **Collections**: 
   - `users` - User accounts and authentication
   - `profiles` - User profiles
   - `partnerPreferences` - Partner search preferences
   - `photos` - User photos
   - `interests` - Connection requests
   - `messages` - Private messages

### 🚀 Quick Start

1. **Restore packages** (this will install MongoDB.Driver):
```bash
cd MatrimonyAPI
dotnet restore
```

2. **Run the application**:
```bash
dotnet run
```

3. **Access Swagger UI**:
```
https://localhost:5001/swagger
```

That's it! **No database migrations needed** - MongoDB will automatically create the database and collections on first use.

### 🔌 MongoDB Connection Details

Your connection string is already configured in `appsettings.json`:

```json
{
  "MongoDB": {
    "ConnectionString": "mongodb+srv://matbeadmin:ehRGqfauSavFP31P@cluster.ko8ipm8.mongodb.net/?appName=Cluster",
    "DatabaseName": "MatrimonyDB"
  }
}
```

**Important**: This connection string contains credentials. For production:
1. Move credentials to environment variables
2. Use Azure Key Vault or AWS Secrets Manager
3. Never commit credentials to source control

### 📊 Database Structure

#### Users Collection
```json
{
  "_id": "ObjectId",
  "email": "string",
  "passwordHash": "string",
  "phoneNumber": "string",
  "createdAt": "DateTime",
  "lastLoginAt": "DateTime",
  "isActive": "boolean",
  "isEmailVerified": "boolean",
  "isPhoneVerified": "boolean",
  "role": "string"
}
```

#### Profiles Collection
```json
{
  "_id": "ObjectId",
  "userId": "ObjectId",
  "firstName": "string",
  "lastName": "string",
  "dateOfBirth": "DateTime",
  "gender": "string",
  "religion": "string",
  "community": "string",
  "motherTongue": "string",
  "maritalStatus": "string",
  "heightInCm": "number",
  "education": "string",
  "occupation": "string",
  "annualIncome": "decimal",
  "country": "string",
  "state": "string",
  "city": "string",
  "about": "string",
  "familyDetails": "string",
  "hobbies": "string",
  "isProfileComplete": "boolean",
  "updatedAt": "DateTime"
}
```

### 🔍 Indexes

The application automatically creates the following indexes on startup:

1. **Users**: Unique index on `email`
2. **Profiles**: Unique index on `userId`
3. **Profiles**: Compound index on `gender`, `religion`, `city` (for search)
4. **Photos**: Index on `userId`
5. **Interests**: Indexes on `senderId` and `receiverId`
6. **Messages**: Indexes on `senderId` and `receiverId`
7. **PartnerPreferences**: Unique index on `userId`

### 🧪 Testing the API

1. **Register a user**:
```bash
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test@123",
    "phoneNumber": "+1234567890"
  }'
```

2. **Login**:
```bash
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test@123"
  }'
```

3. **Use the token** in Swagger or in subsequent requests:
```
Authorization: Bearer YOUR_TOKEN_HERE
```

### 🔧 Configuration Options

You can override settings using environment variables:

**Windows PowerShell:**
```powershell
$env:MongoDB__ConnectionString = "your-connection-string"
$env:MongoDB__DatabaseName = "YourDatabaseName"
```

**Linux/Mac:**
```bash
export MongoDB__ConnectionString="your-connection-string"
export MongoDB__DatabaseName="YourDatabaseName"
```

### 📈 Monitoring Your Database

1. **MongoDB Atlas Dashboard**: https://cloud.mongodb.com
2. **Login credentials**: Use the account that owns the cluster
3. **View collections**: Browse Data → MatrimonyDB
4. **Monitor performance**: Metrics tab
5. **View logs**: Logs tab

### 🔒 Security Best Practices

1. **Network Access**: Configure IP whitelist in MongoDB Atlas
2. **Database Users**: Create separate users for dev/staging/prod
3. **Connection String**: Use environment variables in production
4. **SSL/TLS**: Enabled by default with MongoDB Atlas
5. **Authentication**: JWT tokens expire after 7 days

### 🐛 Troubleshooting

#### Connection Fails
- Check internet connection
- Verify MongoDB Atlas cluster is running
- Check IP whitelist in MongoDB Atlas (add your IP or use 0.0.0.0/0 for development)

#### Collections Not Created
- Collections are created automatically on first insert
- Check MongoDB Atlas dashboard to verify

#### Slow Queries
- Check indexes are created (view in Atlas dashboard)
- Monitor slow queries in Atlas Performance tab
- Consider adding custom indexes for specific queries

### 📚 Key Differences from SQL Server

1. **No Migrations Needed**: Collections are created automatically
2. **Schema Flexibility**: Fields can be added without schema changes
3. **ObjectId**: Uses MongoDB ObjectId instead of integers for IDs
4. **No JOINs**: Data is retrieved separately (but efficiently with indexes)
5. **Filtering**: Uses MongoDB filter builders instead of LINQ

### 💡 Development Tips

1. **MongoDB Compass**: Download for GUI database management
   - https://www.mongodb.com/products/compass

2. **View Generated Queries**: Add logging:
```json
{
  "Logging": {
    "LogLevel": {
      "MongoDB": "Debug"
    }
  }
}
```

3. **Test Locally**: Use MongoDB Community Edition or Docker:
```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

Then update connection string:
```json
"ConnectionString": "mongodb://localhost:27017"
```

### 📦 Project Benefits with MongoDB

✅ **No Schema Migrations** - Deploy faster  
✅ **Flexible Schema** - Easy to add new fields  
✅ **Cloud-Ready** - MongoDB Atlas included  
✅ **Scalable** - Built for horizontal scaling  
✅ **JSON Native** - Perfect for REST APIs  
✅ **Powerful Queries** - Rich query language  

### 🎯 Next Steps

1. ✅ ~~Configure MongoDB~~ (Complete!)
2. ✅ ~~Update all models~~ (Complete!)
3. ✅ ~~Update all services~~ (Complete!)
4. ✅ ~~Configure indexes~~ (Complete!)
5. 🚀 **Start building your frontend!**

---

**Ready to go!** Your matrimony API is now powered by MongoDB Atlas. 🎉

For any issues, check:
- MongoDB Atlas Status: https://status.cloud.mongodb.com
- MongoDB Documentation: https://docs.mongodb.com
- Driver Documentation: https://mongodb.github.io/mongo-csharp-driver/

