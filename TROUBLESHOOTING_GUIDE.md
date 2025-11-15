# 🔧 TROUBLESHOOTING GUIDE - Common Issues & Solutions

This guide covers all common issues you might encounter and their solutions.

---

## 🚨 Build & Compilation Errors

### Error 1: "The type or namespace name 'DbContext' could not be found"

**Cause**: Entity Framework not installed

**Solution**:
```powershell
# In Package Manager Console
Install-Package EntityFramework -Version 6.4.4
```

---

### Error 2: "The type or namespace name 'JwtSecurityToken' could not be found"

**Cause**: JWT packages not installed

**Solution**:
```powershell
Install-Package System.IdentityModel.Tokens.Jwt -Version 6.21.0
Install-Package Microsoft.IdentityModel.Tokens -Version 6.21.0
```

---

### Error 3: "Could not load file or assembly 'Newtonsoft.Json'"

**Cause**: Version mismatch

**Solution**:
```powershell
Update-Package Newtonsoft.Json -Reinstall
```

---

### Error 4: "The name 'EnableCors' does not exist in the current context"

**Cause**: CORS package not installed

**Solution**:
```powershell
Install-Package Microsoft.AspNet.WebApi.Cors -Version 5.2.9
```

Then add to `WebApiConfig.cs`:
```csharp
config.EnableCors();
```

---

### Error 5: Multiple assembly binding errors

**Cause**: Version conflicts in Web.config

**Solution**: Update `<runtime>` section in Web.config with proper binding redirects:
```xml
<runtime>
  <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
    <dependentAssembly>
      <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" />
      <bindingRedirect oldVersion="0.0.0.0-13.0.0.0" newVersion="13.0.0.0" />
    </dependentAssembly>
    <!-- Add more as needed -->
  </assemblyBinding>
</runtime>
```

---

## 🗄️ Database Errors

### Error 6: "Cannot open database 'PetrolPumpDB' requested by the login"

**Cause**: Database not created

**Solution**:
```powershell
# In Package Manager Console
Update-Database -Verbose
```

If still fails:
```powershell
Drop-Database
Update-Database
```

---

### Error 7: "Enable-Migrations : The term 'Enable-Migrations' is not recognized"

**Cause**: Entity Framework not properly installed

**Solution**:
```powershell
# Reinstall Entity Framework
Uninstall-Package EntityFramework
Install-Package EntityFramework -Version 6.4.4

# Now try again
Enable-Migrations
```

---

### Error 8: "A network-related or instance-specific error occurred"

**Cause**: LocalDB not running

**Solution**:

**Method 1**: Start LocalDB manually
```cmd
# Open Command Prompt as Administrator
sqllocaldb start MSSQLLocalDB
```

**Method 2**: Change connection string in Web.config to SQL Server Express:
```xml
<add name="DefaultConnection" 
     connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=PetrolPumpDB;Integrated Security=True;" 
     providerName="System.Data.SqlClient" />
```

---

### Error 9: "Login failed for user"

**Cause**: Connection string authentication issue

**Solution**: Use Integrated Security:
```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PetrolPumpDB;Integrated Security=True;Connect Timeout=30;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

## 🌐 Runtime & API Errors

### Error 10: 404 Not Found on /api/account/login

**Cause**: Web API routes not registered properly

**Solution**: Check `Global.asax.cs` - ensure correct order:
```csharp
protected void Application_Start()
{
    GlobalConfiguration.Configure(WebApiConfig.Register); // MUST BE FIRST
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    BundleConfig.RegisterBundles(BundleTable.Bundles);
}
```

---

### Error 11: 401 Unauthorized on dispensing endpoints

**Cause**: JWT token not sent or invalid

**Solution**:

**Check 1**: Verify token in localStorage
```javascript
// In browser console (F12)
console.log(localStorage.getItem('authToken'));
```

**Check 2**: Verify Authorization header format
```javascript
// Should be: Bearer {token}
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Check 3**: Token might be expired (24 hours validity)
- Clear localStorage
- Login again

---

### Error 12: CORS error in browser console

**Error**: "No 'Access-Control-Allow-Origin' header is present"

**Solution**: Add CORS headers in Web.config:
```xml
<system.webServer>
  <httpProtocol>
    <customHeaders>
      <add name="Access-Control-Allow-Origin" value="*" />
      <add name="Access-Control-Allow-Headers" value="Content-Type, Authorization" />
      <add name="Access-Control-Allow-Methods" value="GET, POST, PUT, DELETE, OPTIONS" />
    </customHeaders>
  </httpProtocol>
</system.webServer>
```

---

### Error 13: 500 Internal Server Error on API calls

**Cause**: Exception in controller code

**Solution**:

**Step 1**: Check Visual Studio Output window for detailed error

**Step 2**: Enable detailed errors in Web.config:
```xml
<system.web>
  <customErrors mode="Off"/>
</system.web>
```

**Step 3**: Add try-catch in controller methods:
```csharp
try {
    // Your code
} catch (Exception ex) {
    return InternalServerError(ex);
}
```

---

## 📁 File Upload Errors

### Error 14: "Could not find a part of the path .../Uploads/..."

**Cause**: Uploads folder doesn't exist

**Solution**:

**Method 1**: Create folder manually
1. Right-click project in Solution Explorer
2. Add → New Folder
3. Name: `Uploads`

**Method 2**: Create programmatically (already in code):
```csharp
var uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
if (!Directory.Exists(uploadPath))
{
    Directory.CreateDirectory(uploadPath);
}
```

---

### Error 15: "Maximum request length exceeded"

**Cause**: File size too large (default 4MB)

**Solution**: Increase limit in Web.config:
```xml
<system.web>
  <httpRuntime maxRequestLength="10240" /> <!-- 10MB in KB -->
</system.web>

<system.webServer>
  <security>
    <requestFiltering>
      <requestLimits maxAllowedContentLength="10485760" /> <!-- 10MB in bytes -->
    </requestFiltering>
  </security>
</system.webServer>
```

---

### Error 16: File uploads but can't be viewed

**Cause**: Incorrect path or IIS blocking file types

**Solution**:

**Check 1**: Verify file path in database
- Should be: `/Uploads/filename.ext`
- Not: `C:\Full\Path\...`

**Check 2**: Add static file handler in Web.config:
```xml
<system.webServer>
  <staticContent>
    <mimeMap fileExtension=".pdf" mimeType="application/pdf" />
    <mimeMap fileExtension=".jpg" mimeType="image/jpeg" />
    <mimeMap fileExtension=".png" mimeType="image/png" />
  </staticContent>
</system.webServer>
```

---

## 🎨 Frontend Errors

### Error 17: Bootstrap styles not loading

**Cause**: CDN link broken or blocked

**Solution**: Verify CDN links in .cshtml files:
```html
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
```

---

### Error 18: JavaScript fetch fails with "Failed to fetch"

**Cause**: API URL incorrect or API not running

**Solution**:

**Check 1**: Verify API_BASE_URL in JavaScript:
```javascript
const API_BASE_URL = '/api'; // Relative URL
```

**Check 2**: Test API directly in browser:
```
http://localhost:{port}/api/account/test
```

**Check 3**: Check browser console for CORS errors

---

### Error 19: "localStorage is not defined"

**Cause**: Using localStorage in non-browser context

**Solution**: Only use localStorage in browser JavaScript:
```javascript
if (typeof(Storage) !== "undefined") {
    localStorage.setItem('authToken', token);
}
```

---

## 🔐 Authentication Issues

### Error 20: Login succeeds but redirects to login again

**Cause**: Token not properly stored

**Solution**: Verify token storage:
```javascript
// After successful login
localStorage.setItem('authToken', data.token);
localStorage.setItem('username', username);

// Check if stored
console.log(localStorage.getItem('authToken'));
```

---

### Error 21: "Invalid or expired token" immediately after login

**Cause**: JWT secret key mismatch or token validation issue

**Solution**: Check `JwtTokenHelper.cs`:
```csharp
// Ensure same SecretKey is used for both generation and validation
private const string SecretKey = "YourSuperSecretKeyForJWTToken12345678901234567890";
```

Key must be at least 32 characters long.

---

## 🏃 Runtime Performance Issues

### Error 22: Slow first request after starting application

**Cause**: Normal - IIS Express and .NET Framework compile on first request

**Solution**: This is expected behavior. Subsequent requests will be faster.

---

### Error 23: Application crashes or restarts frequently

**Cause**: Web.config changes trigger app restart

**Solution**: Avoid changing Web.config while debugging. If needed:
1. Stop debugging (Shift+F5)
2. Make changes
3. Start again (F5)

---

## 🔍 Debugging Tips

### Visual Studio Debugging

1. **Set Breakpoints**: Click left margin in code editor
2. **Step Through Code**: F10 (step over), F11 (step into)
3. **Watch Variables**: Debug → Windows → Watch
4. **Output Window**: View → Output (see detailed logs)

### Browser Debugging

1. **Open Developer Tools**: F12
2. **Console Tab**: See JavaScript errors and logs
3. **Network Tab**: See API requests and responses
4. **Application Tab**: Check localStorage

### SQL Debugging

1. **SQL Server Object Explorer**: View → SQL Server Object Explorer
2. **View Data**: Right-click table → View Data
3. **Execute Query**: New Query → Write SQL → Execute

---

## 📞 Still Having Issues?

### Checklist Before Asking for Help:

1. [ ] Error message copied completely
2. [ ] Checked Visual Studio Output window
3. [ ] Checked browser console (F12)
4. [ ] Verified all NuGet packages installed
5. [ ] Tried cleaning and rebuilding (Build → Clean, then Build → Rebuild)
6. [ ] Restarted Visual Studio
7. [ ] Checked this troubleshooting guide

### How to Report an Issue:

Include:
1. **Complete error message**
2. **Stack trace** (if available)
3. **Steps to reproduce**
4. **Visual Studio version**
5. **.NET Framework version**
6. **What you've already tried**

---

## 🔄 Nuclear Option: Start Fresh

If all else fails:

```powershell
# 1. Drop database
Drop-Database

# 2. Delete bin and obj folders
# In Windows Explorer, delete:
# - bin/
# - obj/

# 3. Clean solution
# Build → Clean Solution

# 4. Restore packages
# Right-click Solution → Restore NuGet Packages

# 5. Rebuild
# Build → Rebuild Solution

# 6. Create database
Update-Database

# 7. Run
# Press F5
```

---

This should resolve 99% of issues. If you're still stuck, review the COMPLETE_IMPLEMENTATION_GUIDE.md for detailed setup instructions.
