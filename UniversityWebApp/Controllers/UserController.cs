using BL.Services;
using DAL.Models;
using DAL.ViewModels;
using System.Web.Mvc;
using UniversityWebApp.Helper;

namespace UniversityWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        public ActionResult Register() => View();

        [HttpPost]
        public JsonResult Register(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return Json(new { result = false, errors = ErrorHelper.ModelStateErrorsToDict(ModelState) });

            User user = _userService.Register(userViewModel.Email, userViewModel.Password);
            if (user == null) return Json(new { result = false, url = Url.Action("Login", "User") });
            return Json(new { result = true, url = Url.Action("Login", "User") });
        }

        public ActionResult Login() => View();

        [HttpPost]
        public JsonResult Authenticate(LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid) return Json(new { result = false, errors = ErrorHelper.ModelStateErrorsToDict(ModelState) });

            User user = _userService.Authenticate(loginUserViewModel);
            if (user == null) return Json(new { result = false });
            this.Session["CurrentUser"] = user;
            this.Session["CurrentRole"] = user.Role;
            this.Session["CurrentUserId"] = user.UserId;
            if (user.Role == Role.Admin) return Json(new { result = true, url = Url.Action("Admin", "Home") });
            var url = _userService.isUserRegisteredStudent(user.UserId) ? Url.Action("Index", "Home") : Url.Action("Register", "Student");
            return Json(new { result = true, url });
        }

        public JsonResult GetUser() => Json(new { user = this.Session["CurrentUser"] }, JsonRequestBehavior.AllowGet);

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }

        public ActionResult Index() => View();
    }
}