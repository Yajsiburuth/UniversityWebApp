using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UniversityWebApp.Models;
using UniversityWebApp.Repositories;
using UniversityWebApp.Services;
using UniversityWebApp.ViewModels;

namespace UniversityWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService = new UserService(new UserRepository());
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel userVm)
        {
            if (_userService.GetUser(userVm.Email) != null)
                ViewBag.Error = "Email already in use";

            if (ViewBag.Error == null)
            {
                User user = _userService.Register(userVm.Email, userVm.Password);
                ViewBag.Success = "Registration Successful";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUserViewModel loginUserVm, string ReturnUrl = "")
        {
            User user = _userService.Authenticate(loginUserVm.Email, loginUserVm.Password);
            
            if (user == null)
            {
                ViewBag.Error = "Invalid Credentials";
            }

            if (ViewBag.Error == null)
            {
                int timeout = loginUserVm.RememberMe ? 525600 : 20;
                var ticket = new FormsAuthenticationTicket(user.UserId.ToString(), loginUserVm.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            return View();
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