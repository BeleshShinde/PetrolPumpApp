# ⚡ QUICK START GUIDE - Petrol Pump Application

This is a simplified version of the complete implementation guide for faster setup.

## 🎯 For Absolute Beginners

### Part 1: Install Required Software (15 minutes)

1. **Download & Install Visual Studio 2022 Community (FREE)**
   - Link: https://visualstudio.microsoft.com/downloads/
   - During installation, select: ✅ ASP.NET and web development
   - Wait for installation to complete (~30 minutes)

2. **Verify .NET Framework 4.8 is installed**
   - Open Command Prompt
   - Type: `reg query "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release`
   - If not found, download from: https://dotnet.microsoft.com/download/dotnet-framework/net48

### Part 2: Create Project in Visual Studio (10 minutes)

1. Open Visual Studio 2022
2. Click **"Create a new project"**
3. Search: **"ASP.NET Web Application (.NET Framework)"**
4. Click Next
5. **Settings:**
   - Name: `PetrolPumpApp`
   - Framework: `.NET Framework 4.8`
6. Click **Create**
7. Choose **MVC** template
8. ✅ Check **"Web API"**
9. Click **Create** (wait 2-3 minutes)

### Part 3: Install Packages (5 minutes)

Open **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console):

```powershell
Install-Package EntityFramework -Version 6.4.4
Install-Package System.IdentityModel.Tokens.Jwt -Version 6.21.0
Install-Package Microsoft.IdentityModel.Tokens -Version 6.21.0
Install-Package Microsoft.AspNet.WebApi.Cors -Version 5.2.9
```

Wait for all to complete (green checkmarks).

### Part 4: Copy All Code Files (15 minutes)

**I've provided all code files in the `/Code` folder. Here's what to do:**

#### Step-by-Step File Addition:

1. **Models Folder:**
   - Delete existing files in Models folder
   - Copy from `/Code/Models/`:
     - DispensingRecord.cs
     - ApplicationDbContext.cs
     - LoginModel.cs

2. **Controllers Folder:**
   - Delete existing HomeController.cs
   - Copy from `/Code/Controllers/`:
     - AccountController.cs
     - DispensingController.cs
     - HomeController.cs

3. **Create NEW Folders:**
   - Create `Helpers` folder → Copy JwtTokenHelper.cs
   - Create `Filters` folder → Copy JwtAuthenticationAttribute.cs

4. **Views/Home Folder:**
   - Delete all existing .cshtml files
   - Copy from `/Code/Views/`:
     - Index.cshtml
     - Entry.cshtml
     - Listing.cshtml

5. **App_Start Folder:**
   - Replace/Update these files:
     - WebApiConfig.cs
     - RouteConfig.cs
     - FilterConfig.cs
     - BundleConfig.cs

6. **Root Files:**
   - Replace `Web.config` with `/Code/Web.config`
   - Replace `Global.asax.cs` with `/Code/Global.asax.cs`

### Part 5: Setup Database (3 minutes)

Open **Package Manager Console**:

```powershell
Enable-Migrations
Add-Migration InitialCreate
Update-Database
```

**Expected Output**: "Running Seed method" → Success ✅

### Part 6: Create Uploads Folder (1 minute)

1. Right-click project name in Solution Explorer
2. **Add → New Folder**
3. Name it: `Uploads`

### Part 7: Run the Application (2 minutes)

1. Press **F5** or click green **Start** button
2. Browser opens automatically
3. **Login with:**
   - Username: `admin`
   - Password: `admin123`

---

## ✅ Testing Checklist

After successful run, test these features:

- [ ] **Login Page**: Can access and see login form
- [ ] **Authentication**: Can login with admin/admin123
- [ ] **Add Record**: Can fill form and upload file
- [ ] **View Records**: Can see all records in table
- [ ] **Filter by Dispenser**: Filter works correctly
- [ ] **Filter by Payment Mode**: Filter works correctly
- [ ] **Filter by Date**: Date range filter works
- [ ] **View Payment Proof**: Can click and view uploaded file
- [ ] **Logout**: Clears session and redirects to login

---

## 🆘 Common Issues & Quick Fixes

### Issue 1: Build Errors

**Error**: "The name 'DbContext' does not exist"

**Fix:**
```powershell
Install-Package EntityFramework -Version 6.4.4
```

---

### Issue 2: Package Restore Failed

**Error**: "Package restore failed"

**Fix:**
1. Tools → Options → NuGet Package Manager
2. Check "Allow NuGet to download missing packages"
3. Right-click Solution → Restore NuGet Packages

---

### Issue 3: Database Connection Error

**Error**: "Cannot open database 'PetrolPumpDB'"

**Fix:**
```powershell
# In Package Manager Console
Drop-Database
Update-Database -Verbose
```

---

### Issue 4: API Returns 404

**Error**: API calls return 404 Not Found

**Fix**: Check `Global.asax.cs` - ensure this order:
```csharp
protected void Application_Start()
{
    GlobalConfiguration.Configure(WebApiConfig.Register); // FIRST
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes); // AFTER API
    BundleConfig.RegisterBundles(BundleTable.Bundles);
}
```

---

### Issue 5: File Upload Fails

**Error**: "Could not find a part of the path"

**Fix:**
1. Ensure `Uploads` folder exists in project root
2. Right-click Uploads folder → Properties
3. Set "Copy to Output Directory" to "Copy if newer"

---

### Issue 6: CORS Error in Browser Console

**Error**: "No 'Access-Control-Allow-Origin' header"

**Fix**: Verify `Web.config` has CORS configuration:
```xml
<httpProtocol>
  <customHeaders>
    <add name="Access-Control-Allow-Origin" value="*" />
    <add name="Access-Control-Allow-Headers" value="Content-Type, Authorization" />
  </customHeaders>
</httpProtocol>
```

---

### Issue 7: JWT Token Invalid

**Error**: 401 Unauthorized on protected endpoints

**Fix:**
- Clear browser localStorage: F12 → Console → `localStorage.clear()`
- Login again
- Token expires after 24 hours

---

## 📞 Need More Help?

1. Check `COMPLETE_IMPLEMENTATION_GUIDE.md` for detailed explanations
2. Check `README.md` for API documentation
3. Review error messages in:
   - Visual Studio Output window
   - Browser Console (F12)
   - Package Manager Console

---

## 🎉 Success! What's Next?

Once everything works:

1. ✅ Test all features thoroughly
2. ✅ Take screenshots for documentation
3. ✅ Push to GitHub (see GitHub Setup section below)
4. ✅ Share repository link with Blackbox

---

## 🌐 GitHub Setup (5 minutes)

### Step 1: Initialize Git

Open terminal in project folder:

```bash
git init
git add .
git commit -m "Initial commit: Petrol Pump Dispensing Log Application"
```

### Step 2: Create GitHub Repository

1. Go to https://github.com/new
2. Repository name: `petrol-pump-dispensing-app`
3. Description: "Fuel dispensing log system with JWT authentication - Blackbox Coding Challenge"
4. Public
5. **Don't** initialize with README
6. Click **Create repository**

### Step 3: Push to GitHub

```bash
# Replace YOUR_USERNAME with your GitHub username
git remote add origin https://github.com/YOUR_USERNAME/petrol-pump-dispensing-app.git
git branch -M main
git push -u origin main
```

### Step 4: Verify

- Visit your repository URL
- Ensure all files are uploaded
- README.md displays correctly

---

## 📧 Submission to Blackbox

Include in your email:

1. **GitHub Repository Link**
2. **Brief Description**: "Full-stack petrol pump dispensing log application with JWT authentication, CRUD operations, filtering, and file upload"
3. **Tech Stack**: ASP.NET Framework 4.8 MVC, Web API, SQL Server, Entity Framework 6
4. **Test Credentials**: Username: admin, Password: admin123
5. **Key Features**: Authentication, CRUD, Filtering, File Upload
6. **Setup Time**: ~5 minutes with provided instructions

---

## ⏰ Timeline Summary

- Software Installation: 30 minutes (one-time)
- Project Setup: 10 minutes
- Package Installation: 5 minutes
- Code Integration: 15 minutes
- Database Setup: 3 minutes
- Testing: 10 minutes
- GitHub Push: 5 minutes

**Total: ~1.5 hours** (excluding software download/installation)

---

Good luck with your interview! 🚀
