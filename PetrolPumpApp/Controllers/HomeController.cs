using System.Web.Mvc;

namespace PetrolPumpApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entry()
        {
            return View();
        }

        public ActionResult Listing()
        {
            return View();
        }
    }
}
