using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController
        public ActionResult Books()
        {
            return RedirectToAction("index","Book");
        }

        // GET: HomeController
        public ActionResult Authors()
        {
            return RedirectToAction("index","Author");
        }
    }
}
