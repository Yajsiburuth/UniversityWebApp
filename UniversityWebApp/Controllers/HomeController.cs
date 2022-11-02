using DAL.Models;
using System.Web.Mvc;

namespace UniversityWebApp.Controllers
{
    public class HomeController : Controller
    { 
        public ActionResult Index()
        {
            var loggedUser = Session["CurrentUser"] as User;
            var view = View(loggedUser);
            return view;
        }

        public ActionResult Admin()
        {
            if (Session == null)
                return RedirectToAction("Login", "User");
            if (Session["CurrentRole"].ToString() != "Admin")
                return RedirectToAction("Index", "Home");
            return View();
        }
    }
}