# 🚀 COMPLETE IMPLEMENTATION STEPS

Follow these steps exactly to implement the Petrol Pump Application.

---

## STEP 1: INSTALL SOFTWARE (30 minutes - ONE TIME)

### 1.1 Download Visual Studio 2022
- Visit: https://visualstudio.microsoft.com/downloads/
- Download: **Community Edition (FREE)**
- Run installer

### 1.2 Select Workloads
During installation, check:
- ✅ **ASP.NET and web development**
- ✅ **Data storage and processing**

Wait for installation to complete (~30 minutes).

---

## STEP 2: CREATE PROJECT (5 minutes)

### 2.1 Launch Visual Studio
- Open Visual Studio 2022
- Click **"Create a new project"**

### 2.2 Select Template
- Search: **"ASP.NET Web Application (.NET Framework)"**
- Click **Next**

### 2.3 Configure Project
- **Project name**: `PetrolPumpApp`
- **Location**: Choose your folder
- **Framework**: `.NET Framework 4.8`
- Click **Create**

### 2.4 Select MVC + Web API
- Template: **MVC**
- ✅ Check **"Web API"**
- Authentication: **No Authentication**
- Click **Create**

Wait 2-3 minutes for project creation.

---

## STEP 3: INSTALL NUGET PACKAGES (3 minutes)

### 3.1 Open Package Manager Console
- Go to: **Tools → NuGet Package Manager → Package Manager Console**

### 3.2 Install Packages (Run one by one)

```powershell
Install-Package EntityFramework -Version 6.4.4
```
Wait for green checkmark ✅

```powershell
Install-Package System.IdentityModel.Tokens.Jwt -Version 6.21.0
```
Wait for green checkmark ✅

```powershell
Install-Package Microsoft.IdentityModel.Tokens -Version 6.21.0
```
Wait for green checkmark ✅

```powershell
Install-Package Microsoft.AspNet.WebApi.Cors -Version 5.2.9
```
Wait for green checkmark ✅

All packages should install successfully.

---

## STEP 4: ADD CODE FILES (20 minutes)

### 4.1 Models Folder

**Delete existing files:**
- Delete everything in Models folder

**Add new files:**

Right-click **Models** → **Add → Class**

**File 1: DispensingRecord.cs**
- Copy content from `/Code/Models/DispensingRecord.cs`

**File 2: ApplicationDbContext.cs**
- Copy content from `/Code/Models/ApplicationDbContext.cs`

**File 3: LoginModel.cs**
- Copy content from `/Code/Models/LoginModel.cs`

---

### 4.2 Create Helpers Folder

Right-click **Project** → **Add → New Folder** → Name: `Helpers`

**Add file:**
- Right-click **Helpers** → **Add → Class**
- Name: `JwtTokenHelper.cs`
- Copy content from `/Code/Helpers/JwtTokenHelper.cs`

---

### 4.3 Create Filters Folder

Right-click **Project** → **Add → New Folder** → Name: `Filters`

**Add file:**
- Right-click **Filters** → **Add → Class**
- Name: `JwtAuthenticationAttribute.cs`
- Copy content from `/Code/Filters/JwtAuthenticationAttribute.cs`

---

### 4.4 Controllers Folder

**Delete HomeController.cs** (we'll replace it)

Right-click **Controllers** → **Add → Controller**
- Choose: **Web API 2 Controller - Empty**
- Name: `AccountController`
- Copy content from `/Code/Controllers/AccountController.cs`

Repeat for:
- `DispensingController.cs`
- `HomeController.cs` (MVC Controller this time)

---

### 4.5 Views Folder

Navigate to **Views/Home**

**Delete all existing .cshtml files**

Right-click **Views/Home** → **Add → View**

**Add 3 views:**
1. **Index.cshtml** (Login page)
2. **Entry.cshtml** (Add record page)
3. **Listing.cshtml** (View records page)

Copy content from respective files in `/Code/Views/`

---

### 4.6 App_Start Folder

Update these files with content from `/Code/AppStart/`:

- **WebApiConfig.cs** (replace entire content)
- **RouteConfig.cs** (replace entire content)  
- **FilterConfig.cs** (replace entire content)
- **BundleConfig.cs** (replace entire content)

---

### 4.7 Root Files

**Web.config:**
- Double-click to open
- Replace ENTIRE content with `/Code/Web.config`

**Global.asax.cs:**
- Double-click to open
- Replace ENTIRE content with `/Code/Global.asax.cs`

---

### 4.8 Create Uploads Folder

Right-click **Project** → **Add → New Folder** → Name: `Uploads`

This folder stores uploaded payment proof files.

---

## STEP 5: BUILD PROJECT (1 minute)

### 5.1 Build Solution
- Click: **Build → Build Solution**
- Or press: **Ctrl+Shift+B**

### 5.2 Check for Errors
- Look at **Error List** window
- Should show: **0 Errors, 0 Warnings**
- If errors appear, check TROUBLESHOOTING_GUIDE.md

---

## STEP 6: SETUP DATABASE (2 minutes)

### 6.1 Enable Migrations
In **Package Manager Console**:

```powershell
Enable-Migrations
```

Expected output: "Migrations enabled..."

---

### 6.2 Create Initial Migration

```powershell
Add-Migration InitialCreate
```

Expected output: "Scaffolding migration 'InitialCreate'..."

---

### 6.3 Create Database

```powershell
Update-Database
```

Expected output: "Running Seed method" ✅

---

### 6.4 Verify Database Created

**Option 1: Visual Studio**
- View → SQL Server Object Explorer
- Expand: **(localdb)\MSSQLLocalDB**
- Expand: **Databases**
- You should see: **PetrolPumpDB** ✅

**Option 2: SQL Server Management Studio**
- Connect to: `(localdb)\MSSQLLocalDB`
- Check Databases folder

---

## STEP 7: RUN APPLICATION (1 minute)

### 7.1 Start Debugging
- Press **F5**
- Or click green **Start** button

### 7.2 Browser Opens
- URL: `http://localhost:{port}/Home/Index`
- You should see the **Login Page** ✅

---

## STEP 8: TEST APPLICATION (10 minutes)

### Test 1: Login ✅
1. Username: `admin`
2. Password: `admin123`
3. Click **Login**
4. Should redirect to Listing page

---

### Test 2: Add New Record ✅
1. Click **"Add New Record"** button
2. Fill form:
   - Dispenser: D-01
   - Quantity: 25.50
   - Vehicle: MH12AB1234
   - Payment Mode: Cash
   - Upload any image/PDF
3. Click **Save Record**
4. Should save and redirect to listing

---

### Test 3: View Records ✅
1. Should see your record in table
2. All fields displayed correctly
3. "View" button appears for payment proof

---

### Test 4: Filter Records ✅
1. Select **Dispenser: D-01**
2. Click **Apply**
3. Should show only D-01 records

---

### Test 5: View Payment Proof ✅
1. Click **"View"** button
2. File opens in new tab

---

### Test 6: Logout ✅
1. Click **Logout**
2. Redirects to login page
3. Cannot access protected pages

---

## STEP 9: GITHUB SETUP (5 minutes)

### 9.1 Initialize Git
Open **Command Prompt** in project folder:

```bash
cd path\to\PetrolPumpApp
git init
```

---

### 9.2 Add .gitignore
Create file `.gitignore` in project root with this content:

```
bin/
obj/
.vs/
*.user
*.suo
packages/
App_Data/*.mdf
App_Data/*.ldf
Uploads/*
!Uploads/.gitkeep
```

---

### 9.3 First Commit

```bash
git add .
git commit -m "Initial commit: Petrol Pump Dispensing Log Application"
```

---

### 9.4 Create GitHub Repository
1. Go to: https://github.com/new
2. Repository name: `petrol-pump-app`
3. Description: "Fuel dispensing log system - Blackbox Coding Challenge"
4. **Public**
5. Don't initialize with README
6. Click **Create repository**

---

### 9.5 Push to GitHub

```bash
git remote add origin https://github.com/YOUR_USERNAME/petrol-pump-app.git
git branch -M main
git push -u origin main
```

---

### 9.6 Verify
Visit your repository URL and confirm all files uploaded.

---

## STEP 10: SUBMIT TO BLACKBOX (5 minutes)

### 10.1 Prepare Email

**Subject:** Coding Challenge Submission - Petrol Pump Application

**Body:**
```
Dear Blackbox Team,

I am pleased to submit my coding challenge solution.

GitHub Repository: [YOUR_GITHUB_URL]

Tech Stack:
- Backend: ASP.NET Framework 4.8 MVC + Web API
- Frontend: HTML5, CSS3, Bootstrap 5, JavaScript
- Database: Microsoft SQL Server (LocalDB)
- Authentication: JWT

Test Credentials:
Username: admin
Password: admin123

All requirements have been implemented successfully. 
Complete setup instructions are in the README.md file.

Thank you for the opportunity!

Best regards,
Belesh
```

---

### 10.2 Send Email
- Attach repository link
- Include test credentials
- Send before November 17, 2025

---

## ✅ FINAL CHECKLIST

Before submission, verify:

- [ ] Project builds without errors
- [ ] Login works correctly
- [ ] Can add new records
- [ ] Can view all records
- [ ] Filtering works
- [ ] File upload works
- [ ] Payment proofs viewable
- [ ] Logout works
- [ ] Code pushed to GitHub
- [ ] README.md complete
- [ ] Repository is public
- [ ] Email sent to Blackbox

---

## 🎉 CONGRATULATIONS!

You've successfully completed the Petrol Pump Application!

**What you've built:**
✅ Full-stack web application
✅ JWT authentication system
✅ RESTful API endpoints
✅ CRUD operations with EF
✅ File upload functionality
✅ Advanced filtering system
✅ Responsive modern UI
✅ Professional documentation

---

## 📚 REFERENCE DOCUMENTS

If you encounter any issues:

1. **TROUBLESHOOTING_GUIDE.md** - Error solutions
2. **QUICK_START_GUIDE.md** - Simplified instructions
3. **SETUP_CHECKLIST.md** - Step-by-step checklist
4. **PROJECT_SUMMARY.md** - Project overview
5. **README.md** - Main documentation

---

Good luck with your interview! 🚀

You've demonstrated excellent full-stack development skills!
