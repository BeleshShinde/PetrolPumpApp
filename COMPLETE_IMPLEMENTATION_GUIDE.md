# 🚀 PETROL PUMP DISPENSING LOG APPLICATION - COMPLETE IMPLEMENTATION GUIDE

## 📋 PROJECT OVERVIEW
A full-stack web application for logging fuel dispensing details at a petrol pump with authentication, CRUD operations, filtering, and file upload capabilities.

**Tech Stack:**
- **Backend:** ASP.NET Framework 4.8 MVC Web API
- **Frontend:** HTML5, CSS3, Bootstrap 5, Vanilla JavaScript
- **Database:** Microsoft SQL Server
- **Authentication:** JWT (JSON Web Tokens)
- **ORM:** Entity Framework 6

---

## 🛠️ PREREQUISITES

### 1. Install Visual Studio 2022 (Community Edition - FREE)
**Download Link:** https://visualstudio.microsoft.com/downloads/

**During Installation, select:**
- ✅ ASP.NET and web development
- ✅ .NET desktop development
- ✅ Data storage and processing

**Why Visual Studio?**
- .NET Framework projects are best created in Visual Studio (Windows only)
- Includes all necessary templates and tools
- Built-in SQL Server support

### 2. Install SQL Server

**Option A: SQL Server Express (Recommended - FREE)**
- Download: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- Click "Download now" under Express edition
- Run installer with default settings
- **Default Connection String:** `localhost\SQLEXPRESS` or `(localdb)\MSSQLLocalDB`

**Option B: SQL Server LocalDB (Already included with Visual Studio)**
- Automatically installed with Visual Studio
- **Connection String:** `(localdb)\MSSQLLocalDB`
- Perfect for development

### 3. Install SQL Server Management Studio (SSMS) - Optional but Recommended
- Download: https://aka.ms/ssmsfullsetup
- Useful for viewing database, tables, and data directly

---

## 📁 PROJECT STRUCTURE

```
PetrolPumpApp/
├── PetrolPumpApp.sln                    # Solution file
├── PetrolPumpApp/                       # Main MVC Project
│   ├── App_Data/                        # Database files (LocalDB)
│   ├── App_Start/                       # Configuration files
│   │   ├── WebApiConfig.cs
│   │   ├── RouteConfig.cs
│   │   └── FilterConfig.cs
│   ├── Controllers/                     # API & MVC Controllers
│   │   ├── AccountController.cs         # Authentication API
│   │   ├── DispensingController.cs      # Dispensing CRUD API
│   │   └── HomeController.cs            # Serves frontend pages
│   ├── Models/                          # Data Models
│   │   ├── ApplicationDbContext.cs      # Entity Framework DbContext
│   │   ├── DispensingRecord.cs          # Main entity
│   │   ├── LoginModel.cs                # Login DTO
│   │   └── JwtTokenHelper.cs            # JWT utility
│   ├── Views/                           # Frontend HTML pages
│   │   ├── Home/
│   │   │   ├── Index.cshtml             # Login page
│   │   │   ├── Entry.cshtml             # Add record page
│   │   │   └── Listing.cshtml           # View records page
│   │   └── Shared/
│   │       └── _Layout.cshtml           # Master layout
│   ├── Content/                         # CSS files
│   │   └── Site.css
│   ├── Scripts/                         # JavaScript files
│   │   ├── login.js
│   │   ├── entry.js
│   │   └── listing.js
│   ├── Uploads/                         # Payment proof files
│   ├── Web.config                       # Main configuration
│   └── packages.config                  # NuGet packages
└── README.md                            # Project documentation
```

---

## 🚀 STEP-BY-STEP PROJECT SETUP

### Step 1: Create New Project in Visual Studio

1. Open **Visual Studio 2022**
2. Click **"Create a new project"**
3. Search for **"ASP.NET Web Application (.NET Framework)"**
4. Click **Next**
5. **Project Settings:**
   - **Project name:** `PetrolPumpApp`
   - **Location:** Choose your preferred location
   - **Framework:** `.NET Framework 4.8`
   - Click **Create**
6. **Select Template:**
   - Choose **"MVC"**
   - ✅ Check **"Web API"** (to add Web API support)
   - Authentication: **"No Authentication"** (we'll implement JWT manually)
   - Click **Create**

Visual Studio will create the project structure automatically.

---

### Step 2: Install Required NuGet Packages

1. In Visual Studio, go to **Tools → NuGet Package Manager → Package Manager Console**
2. Run these commands one by one:

```powershell
# Entity Framework for database operations
Install-Package EntityFramework -Version 6.4.4

# JWT Authentication
Install-Package System.IdentityModel.Tokens.Jwt -Version 6.21.0
Install-Package Microsoft.IdentityModel.Tokens -Version 6.21.0

# CORS support for API
Install-Package Microsoft.AspNet.WebApi.Cors -Version 5.2.9

# JSON handling
Install-Package Newtonsoft.Json -Version 13.0.3
```

**Wait for all packages to install successfully.**

---

## 💻 COMPLETE CODE IMPLEMENTATION

All code files have been created in the `/Code` folder. Here's what each file does:

### Models (Data Layer)
- **DispensingRecord.cs**: Main entity representing a fuel dispensing record
- **ApplicationDbContext.cs**: Entity Framework database context
- **LoginModel.cs**: DTOs for login request and response

### Helpers & Filters
- **JwtTokenHelper.cs**: JWT token generation and validation utility
- **JwtAuthenticationAttribute.cs**: Custom authentication filter for API endpoints

### Controllers (API & MVC)
- **AccountController.cs**: Handles login and authentication
- **DispensingController.cs**: CRUD operations for dispensing records with filtering
- **HomeController.cs**: Serves frontend pages (Login, Entry, Listing)

### Views (Frontend)
- **Index.cshtml**: Login page with Bootstrap UI
- **Entry.cshtml**: Add new record form with file upload
- **Listing.cshtml**: Display and filter records

### Configuration Files
- **Web.config**: Main configuration with connection strings
- **WebApiConfig.cs**: Web API routing and CORS configuration
- **RouteConfig.cs**: MVC routing configuration
- **Global.asax.cs**: Application startup configuration

---

## 🗄️ DATABASE SETUP

### Step 3: Enable Entity Framework Migrations

After creating the project and adding all code files, open **Package Manager Console** in Visual Studio:

**Tools → NuGet Package Manager → Package Manager Console**

Run these commands:

```powershell
# Enable migrations
Enable-Migrations

# Create initial migration
Add-Migration InitialCreate

# Update database (creates tables)
Update-Database
```

This will:
1. Create the `PetrolPumpDB` database in LocalDB
2. Create the `DispensingRecords` table with proper schema
3. Set up Entity Framework tracking

### Verify Database Creation

**Option 1: Using Visual Studio**
- Go to **View → SQL Server Object Explorer**
- Expand **(localdb)\MSSQLLocalDB → Databases**
- You should see **PetrolPumpDB**
- Expand **Tables** to see **dbo.DispensingRecords**

**Option 2: Using SSMS**
- Open SQL Server Management Studio
- Connect to: `(localdb)\MSSQLLocalDB`
- Navigate to Databases → PetrolPumpDB

---

## ▶️ HOW TO RUN THE PROJECT

### Step 4: Configure Startup Settings

1. In **Solution Explorer**, right-click on **PetrolPumpApp** project
2. Select **Properties**
3. Go to **Web** tab (left side)
4. Under **Servers**, ensure:
   - Server: **IIS Express**
   - Project URL: **http://localhost:{port}/` (note the port number)
5. Click **Save**

### Step 5: Build and Run

1. Press **F5** or click **Start** button (green play icon)
2. Visual Studio will:
   - Compile the project
   - Launch IIS Express
   - Open browser automatically
3. **Default URL**: `http://localhost:{port}/Home/Index`

### Step 6: Create Uploads Folder

**IMPORTANT**: Before testing file uploads, create this folder:

1. In Solution Explorer, right-click on **PetrolPumpApp** project
2. **Add → New Folder**
3. Name it: **Uploads**

This folder will store uploaded payment proof files.

---

## 🧪 TESTING THE APPLICATION

### Test 1: Login Functionality

1. Navigate to: `http://localhost:{port}/Home/Index`
2. You should see the login page
3. Use credentials:
   - **Username**: `admin`
   - **Password**: `admin123`
4. Click **Login**
5. Should redirect to Listing page

**Expected Result**: ✅ Successful login with JWT token stored

### Test 2: Add New Record

1. Click **"Add New Record"** button
2. Fill in the form:
   - **Dispenser No**: D-01
   - **Quantity Filled**: 25.50
   - **Vehicle Number**: MH12AB1234
   - **Payment Mode**: Cash
   - **Payment Proof**: Upload any image (.jpg/.png) or PDF
3. Click **Save Record**

**Expected Result**: ✅ Record saved, redirected to listing page

### Test 3: View All Records

1. On the Listing page, you should see your newly created record
2. Record should display:
   - All field values
   - Formatted date/time
   - Colored badge for payment mode
   - "View" button for payment proof

**Expected Result**: ✅ Record displayed correctly

### Test 4: Filter Records

1. Try filtering by:
   - **Dispenser No**: Select D-01
   - Click **Apply**
2. Should show only D-01 records

3. Try date range filter:
   - Set Start Date and End Date to today
   - Click **Apply**
4. Should show only today's records

**Expected Result**: ✅ Filters work correctly

### Test 5: View Payment Proof

1. Click **"View"** button next to any record
2. Payment proof file should open in new tab

**Expected Result**: ✅ File opens successfully

### Test 6: API Testing with Postman (Optional)

**Login API:**
```
POST http://localhost:{port}/api/account/login
Headers: Content-Type: application/json
Body:
{
  "Username": "admin",
  "Password": "admin123"
}
```

**Get Records API:**
```
GET http://localhost:{port}/api/dispensing
Headers: Authorization: Bearer {token_from_login}
```

---

## 🔍 CODE EXPLANATION

### Authentication Flow

1. **User Login** → AccountController validates credentials
2. **JWT Generation** → JwtTokenHelper creates signed token
3. **Token Storage** → Frontend stores token in localStorage
4. **Protected Requests** → Frontend sends token in Authorization header
5. **Token Validation** → JwtAuthenticationAttribute validates on each API call

### File Upload Mechanism

1. Frontend sends **multipart/form-data** with file
2. Backend uses **MultipartFormDataStreamProvider**
3. File saved with unique GUID name in **Uploads** folder
4. Path stored in database as: `/Uploads/{guid}.{ext}`

### Entity Framework Workflow

```
DbContext → DbSet<DispensingRecord> → SQL Server LocalDB
              ↓
         LINQ Queries
              ↓
        Database Operations
```

### Filtering Logic

```csharp
// Start with all records
var query = db.DispensingRecords.AsQueryable();

// Apply filters conditionally
if (!string.IsNullOrEmpty(dispenserNo))
    query = query.Where(r => r.DispenserNo == dispenserNo);

// Execute query
var results = query.ToList();
```

---

## 🚨 TROUBLESHOOTING

### Issue 1: Database Connection Error

**Error**: "Cannot open database 'PetrolPumpDB'"

**Solution**:
```powershell
# In Package Manager Console
Update-Database -Verbose
```

### Issue 2: 404 Not Found on API Calls

**Problem**: Web API routes not registered

**Solution**: Ensure `Global.asax.cs` registers Web API BEFORE MVC:
```csharp
GlobalConfiguration.Configure(WebApiConfig.Register); // FIRST
RouteConfig.RegisterRoutes(RouteTable.Routes); // SECOND
```

### Issue 3: File Upload Fails

**Problem**: Uploads folder doesn't exist

**Solution**:
1. Create **Uploads** folder in project root
2. Right-click folder → **Properties**
3. Set **Copy to Output Directory**: Copy always

### Issue 4: CORS Error

**Problem**: "No 'Access-Control-Allow-Origin' header"

**Solution**: Verify `Web.config` has CORS headers in `<httpProtocol>` section

### Issue 5: JWT Token Invalid

**Problem**: 401 Unauthorized on API calls

**Solution**:
- Check localStorage has token: `localStorage.getItem('authToken')`
- Verify Authorization header format: `Bearer {token}`
- Token expires after 24 hours - login again

---

## 📁 PROJECT STRUCTURE CHECKLIST

Before running, ensure you have:

```
PetrolPumpApp/
├── ✅ App_Data/
├── ✅ App_Start/
│   ├── ✅ WebApiConfig.cs
│   ├── ✅ RouteConfig.cs
│   ├── ✅ FilterConfig.cs
│   └── ✅ BundleConfig.cs
├── ✅ Controllers/
│   ├── ✅ AccountController.cs
│   ├── ✅ DispensingController.cs
│   └── ✅ HomeController.cs
├── ✅ Filters/
│   └── ✅ JwtAuthenticationAttribute.cs
├── ✅ Helpers/
│   └── ✅ JwtTokenHelper.cs
├── ✅ Models/
│   ├── ✅ DispensingRecord.cs
│   ├── ✅ ApplicationDbContext.cs
│   └── ✅ LoginModel.cs
├── ✅ Views/
│   └── ✅ Home/
│       ├── ✅ Index.cshtml
│       ├── ✅ Entry.cshtml
│       └── ✅ Listing.cshtml
├── ✅ Uploads/ (create this manually)
├── ✅ Web.config
├── ✅ Global.asax
└── ✅ packages.config
```

---

## 🌐 GITHUB SETUP

### Step 7: Initialize Git Repository

Open terminal in project folder:

```bash
# Initialize repository
git init

# Create .gitignore
echo "bin/
obj/
.vs/
*.user
*.suo
packages/
App_Data/*.mdf
App_Data/*.ldf
Uploads/*
!Uploads/.gitkeep" > .gitignore

# Create placeholder for Uploads folder
echo "" > Uploads/.gitkeep

# Stage all files
git add .

# First commit
git commit -m "Initial commit: Petrol Pump Dispensing Log Application"
```

### Step 8: Push to GitHub

1. **Create new repository on GitHub**
   - Go to: https://github.com/new
   - Repository name: `petrol-pump-app`
   - Description: "Fuel dispensing log system with JWT authentication"
   - Public or Private (your choice)
   - **DO NOT** initialize with README (we already have code)
   - Click **Create repository**

2. **Link and push local repository**

```bash
# Add remote origin (replace YOUR_USERNAME)
git remote add origin https://github.com/YOUR_USERNAME/petrol-pump-app.git

# Push code
git branch -M main
git push -u origin main
```

### Step 9: Create Professional README.md

Create `README.md` in project root with this content:

