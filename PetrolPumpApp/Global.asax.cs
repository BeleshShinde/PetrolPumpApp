using PetrolPumpApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PetrolPumpApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Set database initializer
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());

            // Initialize database - ignore errors if table already exists
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Database.Initialize(force: false);

                    var recordCount = db.DispensingRecords.Count();
                    System.Diagnostics.Debug.WriteLine($"Database initialized successfully. Records: {recordCount}");
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Ignore "object already exists" errors
                if (ex.Message.Contains("already an object"))
                {
                    System.Diagnostics.Debug.WriteLine("Database already exists - continuing...");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
                }
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Initialization error: {ex.Message}");
            }

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}