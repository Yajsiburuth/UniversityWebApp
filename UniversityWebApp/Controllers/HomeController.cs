using DAL.Models;
using System.Web.Mvc;

namespace UniversityWebApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult RegisterStudent() => View();

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
            var loggedUser = Session["CurrentUser"] as User;
            var view = View(loggedUser);
            view.MasterName = "~/Views/Shared/_Layout.cshtml";
            return view;
        }

        public JsonResult GetStudentsSummary()
        {
            // TODO FetchStudentsSummary
            return Json(new { });
        }
    }
}