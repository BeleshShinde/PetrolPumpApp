# ✅ SETUP CHECKLIST - Petrol Pump Application

Use this checklist to ensure proper setup. Check each item as you complete it.

## 📋 Pre-Setup Requirements

- [ ] Windows 10/11 installed
- [ ] Internet connection available
- [ ] At least 10GB free disk space

---

## 🔧 Software Installation

- [ ] Visual Studio 2022 Community installed
- [ ] "ASP.NET and web development" workload selected
- [ ] .NET Framework 4.8 SDK installed
- [ ] SQL Server LocalDB available (comes with VS)

**Verify Installation:**
```cmd
dotnet --version
```

---

## 🏗️ Project Creation

- [ ] Created new ASP.NET Web Application (.NET Framework)
- [ ] Project name: `PetrolPumpApp`
- [ ] Framework version: .NET Framework 4.8
- [ ] Template: MVC
- [ ] Web API checkbox enabled
- [ ] Project builds without errors

---

## 📦 NuGet Packages Installed

- [ ] EntityFramework (6.4.4)
- [ ] System.IdentityModel.Tokens.Jwt (6.21.0)
- [ ] Microsoft.IdentityModel.Tokens (6.21.0)
- [ ] Microsoft.AspNet.WebApi.Cors (5.2.9)
- [ ] Newtonsoft.Json (13.0.3)

**Verify in Package Manager Console:**
```powershell
Get-Package
```

---

## 📁 Code Files Added

### Models Folder
- [ ] DispensingRecord.cs
- [ ] ApplicationDbContext.cs
- [ ] LoginModel.cs

### Controllers Folder
- [ ] AccountController.cs
- [ ] DispensingController.cs
- [ ] HomeController.cs (replaced)

### Helpers Folder (Create New)
- [ ] Helpers folder created
- [ ] JwtTokenHelper.cs added

### Filters Folder (Create New)
- [ ] Filters folder created
- [ ] JwtAuthenticationAttribute.cs added

### Views/Home Folder
- [ ] Index.cshtml (Login page)
- [ ] Entry.cshtml (Add record page)
- [ ] Listing.cshtml (View records page)

### App_Start Folder (Updated)
- [ ] WebApiConfig.cs
- [ ] RouteConfig.cs
- [ ] FilterConfig.cs
- [ ] BundleConfig.cs

### Root Files (Updated)
- [ ] Web.config
- [ ] Global.asax.cs

### Additional Folders
- [ ] Uploads folder created (empty for now)

---

## 🗄️ Database Setup

- [ ] Migrations enabled (`Enable-Migrations`)
- [ ] Initial migration created (`Add-Migration InitialCreate`)
- [ ] Database updated (`Update-Database`)
- [ ] PetrolPumpDB visible in SQL Server Object Explorer
- [ ] DispensingRecords table exists

**Check Database:**
1. View → SQL Server Object Explorer
2. Expand (localdb)\MSSQLLocalDB
3. Expand Databases
4. Verify PetrolPumpDB exists

---

## ▶️ Build & Run

- [ ] Solution builds successfully (Build → Build Solution)
- [ ] No compilation errors in Error List
- [ ] Press F5 to run
- [ ] Browser opens automatically
- [ ] Login page displays correctly
- [ ] No console errors in browser (F12)

---

## 🧪 Functionality Testing

### Authentication
- [ ] Login page loads with styled UI
- [ ] Can login with admin/admin123
- [ ] Invalid credentials show error message
- [ ] Successful login redirects to Listing page
- [ ] Token stored in localStorage

### Add New Record
- [ ] Entry page loads
- [ ] All form fields display correctly
- [ ] Dispenser dropdown has D-01 to D-04
- [ ] Payment mode dropdown works
- [ ] File upload accepts .jpg, .png, .pdf
- [ ] Form validation works
- [ ] Record saves successfully
- [ ] Redirects to listing page after save

### View Records
- [ ] Listing page shows all records
- [ ] Table displays correctly with Bootstrap styling
- [ ] Records show: ID, Dispenser, Quantity, Vehicle, Payment Mode, Date, Proof
- [ ] Payment proof "View" button appears
- [ ] Clicking View opens file in new tab

### Filtering
- [ ] Filter by Dispenser Number works
- [ ] Filter by Payment Mode works
- [ ] Filter by Date Range works
- [ ] Filters can be combined
- [ ] Clear Filters button works
- [ ] Total records count updates

### Logout
- [ ] Logout button visible
- [ ] Clicking logout clears token
- [ ] Redirects to login page
- [ ] Cannot access protected pages after logout

---

## 🌐 GitHub Setup

- [ ] Git initialized in project folder
- [ ] .gitignore file added
- [ ] Initial commit created
- [ ] GitHub repository created
- [ ] Remote origin added
- [ ] Code pushed to GitHub
- [ ] Repository is public
- [ ] README.md displays correctly on GitHub

---

## 📸 Documentation

- [ ] Screenshots taken of:
  - Login page
  - Entry page with form filled
  - Listing page with records
  - Filter functionality
  - Payment proof viewing
- [ ] README.md is complete
- [ ] Test credentials documented

---

## 📧 Final Submission Checklist

- [ ] GitHub repository link ready
- [ ] Repository is public (not private)
- [ ] All code pushed to main branch
- [ ] README.md includes:
  - [ ] Project overview
  - [ ] Tech stack
  - [ ] Setup instructions
  - [ ] Test credentials
  - [ ] Features list
  - [ ] API documentation
- [ ] Code is well-formatted
- [ ] No sensitive data in repository
- [ ] Commit messages are meaningful

---

## 🎯 Email to Blackbox

Subject: Coding Challenge Submission - Petrol Pump Dispensing Log Application

Body Template:

```
Dear Blackbox Team,

I am pleased to submit my coding challenge solution for the Petrol Pump Dispensing Log Application.

GitHub Repository: [YOUR_GITHUB_URL]

Tech Stack:
- Backend: ASP.NET Framework 4.8 MVC + Web API
- Frontend: HTML5, CSS3, Bootstrap 5, JavaScript
- Database: Microsoft SQL Server (LocalDB)
- Authentication: JWT (JSON Web Tokens)

Key Features Implemented:
✅ JWT-based authentication with token management
✅ CRUD operations for dispensing records
✅ File upload for payment proofs (JPG, PNG, PDF)
✅ Advanced filtering (Dispenser, Payment Mode, Date Range)
✅ Responsive UI with Bootstrap 5
✅ RESTful API endpoints
✅ Entity Framework 6 with Code-First approach

Test Credentials:
Username: admin
Password: admin123

Setup Instructions:
Complete setup instructions are provided in the README.md file in the repository.
Estimated setup time: 5-10 minutes with Visual Studio 2022.

All requirements from the problem statement have been implemented successfully.

Thank you for the opportunity!

Best regards,
Belesh
```

---

## ✅ Final Quality Check

Before submitting, verify:

- [ ] Code compiles without errors
- [ ] All features work as expected
- [ ] No hardcoded paths (use relative paths)
- [ ] Proper error handling implemented
- [ ] Code is properly commented
- [ ] No debug/test code left in
- [ ] Connection string uses LocalDB (not hardcoded server)
- [ ] README is professional and complete
- [ ] Repository URL is correct in email

---

## 🎉 Submission Complete!

Once all items are checked:
1. ✅ Send email to Blackbox
2. ✅ Relax and wait for feedback
3. ✅ Prepare for technical interview discussion about your implementation

Good luck! 🚀
