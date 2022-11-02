using BL.Services;
using DAL.Models;
using DAL.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            int timeout = 525600;
            var ticket = new FormsAuthenticationTicket(user.UserId.ToString(), true, timeout);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);
            //FormsAuthentication.SetAuthCookie(user.UserId.ToString(), createPersistentCookie: true, cookie);
            this.Session["CurrentUser"] = user;
            this.Session["CurrentRole"] = user.Role;
            this.Session["CurrentUserId"] = user.UserId;
            if (user.Role == Role.Admin) return Json(new { result = true, url = Url.Action("Admin", "Home") });
            var url = _userService.isUserRegisteredStudent(user.UserId) ? Url.Action("Index", "Home") : Url.Action("Register", "Student");
            return Json(new { result = true, url });
        }

        public JsonResult GetUser() => Json(new { user = this.Session["CurrentUser"] }, JsonRequestBehavior.AllowGet);

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }

        public ActionResult Index() => View();
    }
}