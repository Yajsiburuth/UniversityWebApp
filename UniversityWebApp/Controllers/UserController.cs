using BL.Services;
using DAL.Models;
using DAL.Repositories;
using DAL.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace UniversityWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService = new UserService(new UserRepository());

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Register(UserViewModel userVm)
        {
            if (_userService.GetUser(userVm.Email) != null)
                return Json(new { result = false, url = Url.Action("Login", "User") });

            User user = _userService.Register(userVm.Email, userVm.Password);
            return Json(new { result = true, url = Url.Action("Login", "User") });

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginUserViewModel loginUserVm)
        {
            User user = _userService.Authenticate(loginUserVm);
            if (user != null)
            {
                this.Session["CurrentUser"] = user;
                this.Session["CurrentRole"] = user.Role;
                int timeout = loginUserVm.RememberMe ? 525600 : 20;
                var ticket = new FormsAuthenticationTicket(user.UserId.ToString(), loginUserVm.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
            }

            return Json(new { result = user == null ? false : true, url = user != null ? user.Role == Role.User ? Url.Action("Index", "Home") : Url.Action("Admin", "Home") : Url.Action("Index", "Home")});
        }

        [HttpGet]
        public JsonResult GetUser()
        {
            return Json(new { user = this.Session["CurrentUser"] }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}