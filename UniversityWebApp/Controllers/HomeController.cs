using BL.Services;
using DAL.Models;
using System.Web.Mvc;

namespace UniversityWebApp.Controllers
{
    public class HomeController : Controller
    { 
        [Authorize]
        public ActionResult Index()
        {
            var loggedUser = Session["CurrentUser"] as User;
            var view = View(loggedUser);
            return view;
        }

        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }
    }
}