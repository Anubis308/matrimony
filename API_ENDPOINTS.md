# Matrimony API - Complete Endpoint Reference

## Base URL
- Development: `https://localhost:5001/api`
- All endpoints return JSON

## Authentication Required
Most endpoints require JWT authentication. Include the token in the Authorization header:
```
Authorization: Bearer YOUR_JWT_TOKEN
```

---

## 🔐 Authentication Endpoints

### Register New User
```http
POST /api/auth/register
```

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123",
  "phoneNumber": "+1234567890"
}
```

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": 1,
  "email": "user@example.com",
  "isProfileComplete": false
}
```

### Login
```http
POST /api/auth/login
```

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "SecurePassword123"
}
```

**Response (200 OK):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": 1,
  "email": "user@example.com",
  "isProfileComplete": true
}
```

### Change Password
```http
POST /api/auth/change-password
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "oldPassword": "OldPassword123",
  "newPassword": "NewPassword123"
}
```

### Get Current User Info
```http
GET /api/auth/me
Authorization: Bearer {token}
```

---

## 👤 Profile Endpoints

### Create Profile
```http
POST /api/profile
Authorization: Bearer {token}
```

**Request Body:**
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
  "education": "Bachelor's Degree",
  "occupation": "Software Engineer",
  "annualIncome": 75000,
  "country": "USA",
  "state": "California",
  "city": "San Francisco",
  "about": "About me...",
  "familyDetails": "Family details...",
  "hobbies": "Reading, traveling"
}
```

**Enum Values:**
- Gender: 0=Male, 1=Female, 2=Other
- MaritalStatus: 0=NeverMarried, 1=Divorced, 2=Widowed, 3=AwaitingDivorce

### Get My Profile
```http
GET /api/profile/me
Authorization: Bearer {token}
```

### Get Profile by ID
```http
GET /api/profile/{profileId}
Authorization: Bearer {token}
```

### Update Profile
```http
PUT /api/profile
Authorization: Bearer {token}
```

**Request Body:** (All fields optional)
```json
{
  "firstName": "John",
  "education": "Master's Degree",
  "occupation": "Senior Software Engineer"
}
```

### Delete Profile
```http
DELETE /api/profile
Authorization: Bearer {token}
```

### Upload Photo
```http
POST /api/profile/photos
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "url": "https://example.com/photo.jpg",
  "isPrimary": true
}
```

### Delete Photo
```http
DELETE /api/profile/photos/{photoId}
Authorization: Bearer {token}
```

### Set Primary Photo
```http
PUT /api/profile/photos/{photoId}/set-primary
Authorization: Bearer {token}
```

---

## ⚙️ Preference Endpoints

### Create or Update Preferences
```http
POST /api/preference
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "minAge": 25,
  "maxAge": 35,
  "minHeightInCm": 160,
  "maxHeightInCm": 180,
  "preferredReligions": "Christian,Catholic",
  "preferredCommunities": "Catholic,Protestant",
  "preferredMaritalStatus": "NeverMarried,Divorced",
  "preferredEducation": "Bachelor's,Master's",
  "preferredOccupation": "Engineer,Doctor",
  "minAnnualIncome": 50000,
  "preferredCountries": "USA,Canada",
  "preferredStates": "California,New York",
  "preferredCities": "San Francisco,Los Angeles"
}
```

### Get My Preferences
```http
GET /api/preference
Authorization: Bearer {token}
```

### Delete Preferences
```http
DELETE /api/preference
Authorization: Bearer {token}
```

---

## 🔍 Search Endpoints

### Search Profiles
```http
POST /api/search
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "minAge": 25,
  "maxAge": 35,
  "gender": 1,
  "minHeightInCm": 160,
  "maxHeightInCm": 175,
  "religion": "Christian",
  "community": "Catholic",
  "maritalStatus": 0,
  "education": "Bachelor",
  "occupation": "Engineer",
  "minAnnualIncome": 50000,
  "country": "USA",
  "state": "California",
  "city": "San Francisco",
  "pageNumber": 1,
  "pageSize": 20
}
```

**Response:**
```json
{
  "profiles": [
    {
      "id": 1,
      "userId": 2,
      "name": "Jane Smith",
      "age": 28,
      "gender": 1,
      "religion": "Christian",
      "community": "Catholic",
      "maritalStatus": 0,
      "heightInCm": 165,
      "education": "Bachelor's Degree",
      "occupation": "Designer",
      "city": "San Francisco",
      "state": "California",
      "country": "USA",
      "primaryPhotoUrl": "https://example.com/photo.jpg"
    }
  ],
  "totalCount": 45,
  "pageNumber": 1,
  "pageSize": 20,
  "totalPages": 3
}
```

### Get Matches (Based on Preferences)
```http
GET /api/search/matches?pageNumber=1&pageSize=20
Authorization: Bearer {token}
```

---

## 💝 Interest Endpoints

### Send Interest
```http
POST /api/interest
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "receiverId": 2,
  "message": "Hi, I'd like to connect with you!"
}
```

### Respond to Interest
```http
POST /api/interest/respond
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "interestId": 5,
  "status": 1
}
```

**Status Values:** 1=Accepted, 2=Rejected

### Get Sent Interests
```http
GET /api/interest/sent
Authorization: Bearer {token}
```

### Get Received Interests
```http
GET /api/interest/received
Authorization: Bearer {token}
```

**Response:**
```json
[
  {
    "id": 5,
    "senderId": 1,
    "senderName": "John Doe",
    "receiverId": 2,
    "receiverName": "Jane Smith",
    "status": 0,
    "message": "Hi, I'd like to connect!",
    "sentAt": "2024-10-26T10:30:00Z",
    "respondedAt": null
  }
]
```

**Interest Status:** 0=Pending, 1=Accepted, 2=Rejected, 3=Cancelled

### Cancel Interest
```http
DELETE /api/interest/{interestId}
Authorization: Bearer {token}
```

---

## 💬 Message Endpoints

### Send Message
```http
POST /api/message
Authorization: Bearer {token}
```

**Request Body:**
```json
{
  "receiverId": 2,
  "content": "Hello! How are you?"
}
```

**Note:** You can only message users with whom you have an accepted interest connection.

### Get All Conversations
```http
GET /api/message/conversations
Authorization: Bearer {token}
```

**Response:**
```json
[
  {
    "otherUserId": 2,
    "otherUserName": "Jane Smith",
    "lastMessage": "See you soon!",
    "lastMessageTime": "2024-10-26T15:30:00Z",
    "unreadCount": 2
  }
]
```

### Get Conversation with User
```http
GET /api/message/conversation/{otherUserId}
Authorization: Bearer {token}
```

**Response:**
```json
[
  {
    "id": 10,
    "senderId": 1,
    "senderName": "John Doe",
    "receiverId": 2,
    "receiverName": "Jane Smith",
    "content": "Hello!",
    "isRead": true,
    "sentAt": "2024-10-26T14:00:00Z",
    "readAt": "2024-10-26T14:05:00Z"
  }
]
```

### Mark Message as Read
```http
PUT /api/message/{messageId}/read
Authorization: Bearer {token}
```

### Get Unread Count
```http
GET /api/message/unread-count
Authorization: Bearer {token}
```

**Response:**
```json
{
  "unreadCount": 5
}
```

---

## 📊 HTTP Status Codes

- **200 OK** - Request successful
- **201 Created** - Resource created successfully
- **400 Bad Request** - Invalid request data
- **401 Unauthorized** - Missing or invalid authentication token
- **403 Forbidden** - Insufficient permissions
- **404 Not Found** - Resource not found
- **500 Internal Server Error** - Server error

---

## 🔄 Common Response Patterns

### Success Response
```json
{
  "data": { ... },
  "message": "Success"
}
```

### Error Response
```json
{
  "message": "Error description"
}
```

---

## 💡 Usage Tips

1. **Always register/login first** to get a JWT token
2. **Create a profile** before using search and interest features
3. **Set preferences** to get better matches
4. **Accept interests** before you can send messages
5. Use **pagination** for search results to improve performance
6. Token expires after **7 days** - you'll need to login again

---

## 🧪 Testing Workflow

1. Register: `POST /api/auth/register`
2. Login: `POST /api/auth/login` (save the token)
3. Create Profile: `POST /api/profile`
4. Set Preferences: `POST /api/preference`
5. Search: `POST /api/search` or `GET /api/search/matches`
6. Send Interest: `POST /api/interest`
7. (Other user accepts interest)
8. Send Messages: `POST /api/message`

---

## 📝 Notes

- All dates should be in ISO 8601 format: `YYYY-MM-DDTHH:mm:ssZ`
- All numeric IDs are integers
- String fields that accept multiple values use comma-separated format
- Photo URLs should be publicly accessible (or use a file upload service)
- Pagination starts at page 1, not 0

