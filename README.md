# 🚗 Petrol Pump Dispensing Log Application

A full-stack web application for managing and tracking fuel dispensing records at petrol pumps with secure JWT-based authentication.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![Bootstrap](https://img.shields.io/badge/UI-Bootstrap%205-purple)
![License](https://img.shields.io/badge/License-MIT-green)

## 📋 Overview

This application enables petrol pump operators to:
- ✅ Securely log in with JWT authentication
- ✅ Record fuel dispensing details with payment proof upload
- ✅ View all dispensing records in an organized table
- ✅ Filter records by dispenser, payment mode, and date range
- ✅ Download/view payment proof documents

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| **Backend** | ASP.NET Framework 4.8 MVC + Web API |
| **Frontend** | HTML5, CSS3, Bootstrap 5, JavaScript |
| **Database** | Microsoft SQL Server (LocalDB/Express) |
| **ORM** | Entity Framework 6 |
| **Authentication** | JWT (JSON Web Tokens) |
| **File Upload** | Multipart Form Data |

## ✨ Features

### 1. Authentication
- JWT-based stateless authentication
- Token stored in browser localStorage
- 24-hour token expiration
- Protected API endpoints

### 2. Dispensing Records
- **Add New Records**: Form with validation
- **File Upload**: Payment proof (JPG, PNG, PDF)
- **Real-time Validation**: Client and server-side
- **Unique File Naming**: GUID-based to prevent conflicts

### 3. Records Listing & Filtering
- **Responsive Table**: Display all records
- **Multi-filter Support**:
  - Dispenser Number (D-01 to D-04)
  - Payment Mode (Cash, Credit Card, UPI)
  - Date Range (Start & End date)
- **Dynamic Updates**: Filter without page reload
- **File Viewing**: Click to open payment proofs

## 📦 Prerequisites

Before running this project, ensure you have:

- ✅ **Windows Operating System** (for .NET Framework)
- ✅ **Visual Studio 2022** (Community Edition or higher)
  - With "ASP.NET and web development" workload
- ✅ **SQL Server** (LocalDB or Express)
  - LocalDB comes with Visual Studio
- ✅ **.NET Framework 4.8** SDK

## 🚀 Installation & Setup

### Step 1: Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/petrol-pump-app.git
cd petrol-pump-app
```

### Step 2: Open in Visual Studio

1. Launch **Visual Studio 2022**
2. Click **"Open a project or solution"**
3. Navigate to cloned folder
4. Open **`PetrolPumpApp.sln`**

### Step 3: Restore NuGet Packages

Visual Studio will automatically restore packages. If not:
- Right-click on Solution → **Restore NuGet Packages**

### Step 4: Setup Database

Open **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console):

```powershell
# Enable Entity Framework migrations
Enable-Migrations

# Create initial migration
Add-Migration InitialCreate

# Create database and tables
Update-Database
```

This creates a `PetrolPumpDB` database in SQL Server LocalDB.

### Step 5: Create Uploads Folder

1. Right-click on **PetrolPumpApp** project in Solution Explorer
2. **Add → New Folder**
3. Name it: **`Uploads`**

### Step 6: Run the Application

1. Press **F5** or click **Start** (green play button)
2. Browser will open automatically at: `http://localhost:{port}/`

## 🔐 Test Credentials

```
Username: admin
Password: admin123
```

## 📸 Screenshots

### Login Page
Clean and secure JWT authentication interface.

### Add New Record
User-friendly form with file upload for payment proofs.

### Records Listing
Responsive table with filtering capabilities.

## 🗄️ Database Schema

### DispensingRecords Table

| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary Key (Auto-increment) |
| DispenserNo | varchar(50) | Dispenser identifier (D-01 to D-04) |
| QuantityFilled | decimal(18,2) | Fuel quantity in liters |
| VehicleNumber | varchar(50) | Vehicle registration number |
| PaymentMode | varchar(50) | Cash, Credit Card, or UPI |
| PaymentProofPath | varchar(500) | File path to uploaded proof |
| CreatedAt | datetime | Timestamp of record creation |

## 🔌 API Endpoints

### Authentication

```http
POST /api/account/login
Content-Type: application/json

{
  "Username": "admin",
  "Password": "admin123"
}

Response:
{
  "Success": true,
  "Message": "Login successful",
  "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Get All Records (with filtering)

```http
GET /api/dispensing?dispenserNo=D-01&paymentMode=Cash&startDate=2024-01-01&endDate=2024-12-31
Authorization: Bearer {token}

Response:
[
  {
    "Id": 1,
    "DispenserNo": "D-01",
    "QuantityFilled": 25.50,
    "VehicleNumber": "MH12AB1234",
    "PaymentMode": "Cash",
    "PaymentProofPath": "/Uploads/abc123.jpg",
    "CreatedAt": "2024-11-13T10:30:00"
  }
]
```

### Create New Record

```http
POST /api/dispensing
Authorization: Bearer {token}
Content-Type: multipart/form-data

DispenserNo: D-01
QuantityFilled: 25.50
VehicleNumber: MH12AB1234
PaymentMode: Cash
PaymentProof: [file]
```

## 🧪 Testing

### Manual Testing Checklist

- [ ] Login with valid credentials
- [ ] Login with invalid credentials (should fail)
- [ ] Add new dispensing record with file upload
- [ ] View all records in listing page
- [ ] Filter by dispenser number
- [ ] Filter by payment mode
- [ ] Filter by date range
- [ ] View/download payment proof
- [ ] Logout and verify token is cleared

### API Testing with Postman

Import the provided Postman collection for comprehensive API testing.

## 📁 Project Structure

```
PetrolPumpApp/
├── App_Data/              # Database files (LocalDB)
├── App_Start/             # Application configuration
│   ├── WebApiConfig.cs    # API routing & CORS
│   ├── RouteConfig.cs     # MVC routing
│   └── FilterConfig.cs    # Global filters
├── Controllers/           # API & MVC Controllers
│   ├── AccountController.cs
│   ├── DispensingController.cs
│   └── HomeController.cs
├── Filters/               # Custom authentication filters
│   └── JwtAuthenticationAttribute.cs
├── Helpers/               # Utility classes
│   └── JwtTokenHelper.cs
├── Models/                # Data models & DbContext
│   ├── DispensingRecord.cs
│   ├── ApplicationDbContext.cs
│   └── LoginModel.cs
├── Views/                 # Frontend pages
│   └── Home/
│       ├── Index.cshtml   # Login page
│       ├── Entry.cshtml   # Add record page
│       └── Listing.cshtml # View records page
├── Uploads/               # Payment proof storage
└── Web.config             # Main configuration
```

## 🔧 Configuration

### Connection String

Update `Web.config` if using SQL Server Express instead of LocalDB:

```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=PetrolPumpDB;Integrated Security=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### JWT Secret Key

For production, update the secret key in `JwtTokenHelper.cs`:

```csharp
private const string SecretKey = "YourProductionSecretKey123456789";
```

## 🚨 Troubleshooting

### Database Connection Error

```powershell
# Recreate database
Drop-Database
Update-Database
```

### API 404 Errors

Ensure `Global.asax.cs` registers Web API **before** MVC routes.

### File Upload Fails

1. Check `Uploads` folder exists
2. Verify `Web.config` has increased file size limits

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Belesh**
- GitHub: [@YOUR_USERNAME](https://github.com/YOUR_USERNAME)

## 🙏 Acknowledgments

- Built for Blackbox coding challenge
- Bootstrap for UI components
- Font Awesome for icons
- Entity Framework for ORM
- JWT for authentication

---

⭐ **If you find this project useful, please consider giving it a star!**

