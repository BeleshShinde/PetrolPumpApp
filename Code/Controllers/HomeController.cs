using System.Web.Mvc;

namespace PetrolPumpApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index - Login Page
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Entry - Add New Record Page
        public ActionResult Entry()
        {
            return View();
        }

        // GET: Home/Listing - View All Records Page
        public ActionResult Listing()
        {
            return View();
        }
    }
}
