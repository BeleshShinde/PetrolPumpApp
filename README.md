# Petrol Pump Dispensing Log Application

A comprehensive web-based application for managing petrol pump dispensing records with JWT authentication, real-time data visualization, and file upload capabilities.

**Live Repository:** [https://github.com/BeleshShinde/PetrolPumpApp](https://github.com/BeleshShinde/PetrolPumpApp)

---

## 📋 Overview

The Petrol Pump Dispensing Log Application is a full-stack web solution designed to digitize and streamline the management of fuel dispensing records at petrol pumps. The system provides real-time tracking of fuel transactions, secure authentication, and comprehensive reporting capabilities.

**Key Objectives:**
- Digitize manual fuel dispensing record-keeping
- Provide secure access control with JWT authentication
- Enable real-time monitoring of fuel transactions
- Support file attachments for payment verification
- Generate insights through data visualization

---

## ✨ Features

- ✅ **JWT Authentication** - Secure token-based user authentication
- ✅ **CRUD Operations** - Complete Create, Read, Update, Delete for records
- ✅ **File Upload** - Payment proof document uploads (images/PDFs)
- ✅ **Advanced Filtering** - Filter by date, fuel type, payment mode
- ✅ **Real-time Validation** - Client-side and server-side validation
- ✅ **Responsive Design** - Mobile-friendly Bootstrap UI
- ✅ **RESTful API** - Clean API architecture
- ✅ **Entity Framework** - Code-First with automatic migrations

---

## 🛠 Tech Stack & Rationale

### Backend
**ASP.NET Framework 4.8 MVC + Web API**
- *Why?* Enterprise-grade stability, extensive library ecosystem, and proven track record in production environments
- *Benefit:* Mature framework with excellent tooling and debugging support in Visual Studio

**Entity Framework 6**
- *Why?* Powerful ORM that simplifies database operations and supports Code-First migrations
- *Benefit:* Automatic schema management, reduced boilerplate code, and type-safe queries

**SQL Server**
- *Why?* Industry-standard RDBMS with excellent integration with .NET ecosystem
- *Benefit:* ACID compliance, robust transaction support, and enterprise-grade features

### Frontend
**Bootstrap 4**
- *Why?* Responsive design framework ensuring cross-device compatibility
- *Benefit:* Rapid development with pre-built components and mobile-first approach

**JavaScript (ES6) + jQuery**
- *Why?* Universal browser support with modern JavaScript features
- *Benefit:* Simplified AJAX operations and DOM manipulation

### Security
**JWT (JSON Web Tokens)**
- *Why?* Stateless authentication mechanism ideal for RESTful APIs
- *Benefit:* Scalable, no server-side session storage required, works across distributed systems

---

## 📦 Prerequisites

- Visual Studio 2022 (with ASP.NET workload)
- SQL Server 2019+ / SQL Server Express / LocalDB
- .NET Framework 4.8 SDK
- Git (for cloning)
- Modern web browser (Chrome/Firefox/Edge)

---

## 🚀 Setup Instructions

### Step 1: Clone the Repository

```bash
git clone https://github.com/BeleshShinde/PetrolPumpApp.git
cd PetrolPumpApp
```

### Step 2: Configure Database

Open `Web.config` in the project root and choose your connection string:

**For SQL Server (Default Instance):**
```xml
<add name="DefaultConnection"
     connectionString="Data Source=.;Initial Catalog=PetrolPumpDB;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True"
     providerName="System.Data.SqlClient" />
```

**For SQL Server Express:**
```xml
<add name="DefaultConnection"
     connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=PetrolPumpDB;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True"
     providerName="System.Data.SqlClient" />
```

**For LocalDB (Development):**
```xml
<add name="DefaultConnection"
     connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PetrolPumpDB;Integrated Security=True;Connect Timeout=30"
     providerName="System.Data.SqlClient" />
```

### Step 3: Restore NuGet Packages

1. Open the solution in Visual Studio 2022
2. Right-click on Solution → **"Restore NuGet Packages"**
3. Wait for package restoration to complete

### Step 4: Database Setup
💾 Database Setup
Option 1: Automatic Setup (Recommended - Easy)
Entity Framework will automatically create the database for you!
Just configure the connection string and run the application. Entity Framework Code-First will:

✅ Create the database
✅ Create all tables
✅ Set up relationships
✅ Create indexes

Steps:

Update connection string in Web.config (see below)
Press F5 to run the application
Done! Database is created automatically


Option 2: Manual Database Setup (If you prefer manual control)
Step 1: Create Database
Open SQL Server Management Studio (SSMS) and run:
sql-- Create Database
CREATE DATABASE PetrolPumpDB;
GO

USE PetrolPumpDB;
GO
Step 2: Create DispensingRecords Table
sql-- Create DispensingRecords Table
CREATE TABLE DispensingRecords (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DispenserNo NVARCHAR(50) NOT NULL,
    NozzleNo NVARCHAR(50) NULL,
    FuelGrade NVARCHAR(50) NULL,
    Volume DECIMAL(18, 2) NOT NULL DEFAULT 0,
    Amount DECIMAL(18, 2) NOT NULL DEFAULT 0,
    PaymentMode NVARCHAR(50) NOT NULL,
    TransactionDate DATETIME NOT NULL,
    VehicleNumber NVARCHAR(100) NULL,
    ImagePath NVARCHAR(500) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT CK_Volume CHECK (Volume >= 0),
    CONSTRAINT CK_Amount CHECK (Amount >= 0)
);
GO

-- Create Index for better query performance
CREATE INDEX IX_DispensingRecords_TransactionDate 
ON DispensingRecords(TransactionDate DESC);

CREATE INDEX IX_DispensingRecords_DispenserNo 
ON DispensingRecords(DispenserNo);

CREATE INDEX IX_DispensingRecords_FuelGrade 
ON DispensingRecords(FuelGrade);
GO

### Step 5: Build the Solution

```bash
# In Visual Studio:
Press Ctrl+Shift+B
# Or
Build → Build Solution
```

### Step 6: Run the Application

```bash
# Press F5 in Visual Studio
# Or click IIS Express button

# Application will open at: https://localhost:44318
```

### Step 7: Login

**Default Credentials:**
- Username: `admin`
- Password: `admin123`

---

## 🗂 Project Structure

```
PetrolPumpApp/
├── Controllers/
│   ├── HomeController.cs          # MVC views controller
│   └── DispensingController.cs    # API endpoints for CRUD
├── Models/
│   ├── DispensingRecord.cs        # Main entity model
│   ├── ApplicationDbContext.cs    # Entity Framework context
│   ├── LoginModel.cs              # Authentication model
│   └── InMemoryStorage.cs         # In-memory data store
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           # Login page
│   │   └── Entry.cshtml           # Main application interface
│   └── Shared/
│       └── _Layout.cshtml         # Layout template
├── App_Start/
│   ├── RouteConfig.cs             # MVC routing
│   └── WebApiConfig.cs            # Web API configuration
├── Content/                       # CSS files
├── Scripts/                       # JavaScript files
├── Uploads/                       # Payment proof uploads
├── Web.config                     # Application configuration
└── README.md                      # This file
```

---

## 📚 API Documentation

### Authentication

#### Login
```http
POST /Home/Login
Content-Type: application/x-www-form-urlencoded

username=admin&password=admin123
```

**Response:**
```json
{
  "success": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "message": "Login successful"
}
```

### Dispensing Records

#### Get All Records
```http
GET /api/dispensing
Authorization: Bearer {token}
```

#### Get Record by ID
```http
GET /api/dispensing/{id}
Authorization: Bearer {token}
```

#### Create New Record
```http
POST /api/dispensing
Authorization: Bearer {token}
Content-Type: multipart/form-data

{
  "DispenserNo": "D-01",
  "NozzleNo": "N-1",
  "FuelGrade": "Petrol",
  "Volume": 45.5,
  "Amount": 4500.00,
  "PaymentMode": "Credit Card",
  "TransactionDate": "2025-11-15T10:30:00",
  "VehicleNumber": "MH03DP3399",
  "PaymentProof": (file)
}
```

#### Update Record
```http
PUT /api/dispensing/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "Id": 1,
  "DispenserNo": "D-01",
  "FuelGrade": "Petrol",
  ...
}
```

#### Delete Record
```http
DELETE /api/dispensing/{id}
Authorization: Bearer {token}
```

---

## 📝 Assumptions

### Technical Assumptions
1. **SQL Server Availability** - Deployment environment has SQL Server installed and accessible
2. **Windows Environment** - Application is deployed on Windows Server with IIS
3. **Network Access** - SQL Server is accessible from the application server
4. **File Storage** - Local file system is used for payment proof uploads (max 10MB per file)
5. **Authentication** - Single-user system with hardcoded credentials for demonstration purposes

### Business Assumptions
1. **Single Location** - Application serves a single petrol pump location
2. **Manual Entry** - Dispensing data is manually entered (not integrated with fuel dispensers)
3. **Payment Proof** - Optional file upload for transaction verification
4. **No Real-time Integration** - No direct integration with fuel dispensers or payment systems
5. **Basic Reporting** - Simple filtering without complex analytics or dashboards

### Security Assumptions
1. **Internal Network** - Application runs on a trusted internal network
2. **HTTPS in Production** - Production deployment uses HTTPS (configured in IIS)
3. **File Upload Limit** - Maximum 10MB file size for payment proofs
4. **Input Validation** - Both client-side and server-side validation implemented
5. **SQL Injection Prevention** - Prevented through Entity Framework parameterized queries

### Data Assumptions
1. **Volume Unit** - All fuel volumes are measured in liters
2. **Currency** - All amounts are in Indian Rupees (₹ INR)
3. **Date Format** - ISO 8601 format for date/time values
4. **File Formats** - Supported upload formats: JPG, PNG, PDF
5. **Data Retention** - No automatic archival or deletion policies implemented

---

## 📄 License

This project is developed as part of a coding assessment .

---

## 👤 Author

**Belesh Shinde**  
Software Developer

**GitHub:** [@BeleshShinde](https://github.com/BeleshShinde)  
**Repository:** [PetrolPumpApp](https://github.com/BeleshShinde/PetrolPumpApp)

---

## 🙏 Acknowledgments

- ASP.NET Framework and Entity Framework documentation
- Bootstrap team for the responsive design framework
- JWT.io for authentication implementation standards
- Microsoft SQL Server team for robust database platform
- Stack Overflow community for technical support

---

## 📞 Support

For issues, questions, or feature requests:

1. Check the [Issues](https://github.com/BeleshShinde/PetrolPumpApp/issues) page
2. Create a new issue with detailed description
3. Include steps to reproduce (for bugs)
4. Provide environment details (OS, SQL Server version, etc.)

---

## 🎯 Project Highlights

### What Makes This Project Stand Out

**1. Clean Architecture**
- Proper separation of concerns (MVC pattern)
- RESTful API design principles
- Entity Framework Code-First approach

**2. Security First**
- JWT token-based authentication
- Password hashing (prepared for BCrypt implementation)
- SQL injection prevention via ORM
- CORS configuration for controlled access

**3. Production Ready**
- Error handling and logging
- Input validation (client & server)
- File upload with size restrictions
- Responsive design for all devices

**4. Developer Friendly**
- Comprehensive documentation
- Clear setup instructions
- Well-commented code
- Meaningful commit history

**5. Scalability Considerations**
- Stateless authentication (JWT)
- Database connection pooling
- Async/await patterns for better performance
- Migration support for schema changes

---

## 🔧 Troubleshooting

### Common Issues

**Issue:** Cannot connect to SQL Server  
**Solution:** Verify SQL Server is running, check connection string, ensure firewall allows connections

**Issue:** Login fails with correct credentials  
**Solution:** Check if database was created, verify JWT configuration in Web.config

**Issue:** File upload doesn't work  
**Solution:** Ensure Uploads folder exists, check IIS permissions, verify file size limits

**Issue:** API returns 401 Unauthorized  
**Solution:** Verify JWT token is being sent in Authorization header, check token expiration

For detailed troubleshooting, see the [Issues](https://github.com/BeleshShinde/PetrolPumpApp/issues) page.

---

*Last Updated: November 15, 2025 | Version: 1.0.0*

---

⭐ **Star this repository if you find it helpful!**
