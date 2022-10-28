using BL.Services;
using DAL.Models;
using DAL.Repositories;
using DAL.ViewModels;
using System.Web.Mvc;
using System.Web.Security;

namespace UniversityWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController() => _userService = new UserService();
        public UserController(IUserService userService) => _userService = userService;

        [HttpGet]
        public ActionResult Register() => View();

        [HttpPost]
        public JsonResult Register(UserViewModel userViewModel)
        {
            User user = _userService.Register(userViewModel.Email, userViewModel.Password);
            if (user == null) return Json(new { result = false, url = Url.Action("Login", "User") });
            return Json(new { result = true, url = Url.Action("Login", "User") });
        }

        [HttpGet]
        public ActionResult Login() => View();

        [HttpPost]
        public JsonResult Authenticate(LoginUserViewModel loginUserViewModel)
        {
            User user = _userService.Authenticate(loginUserViewModel);
            if (user == null) return Json(new { result = false });
            FormsAuthentication.SetAuthCookie(user.UserId.ToString(), createPersistentCookie: true);
            this.Session["CurrentUser"] = user;
            this.Session["CurrentRole"] = user.Role;
            return Json(new { result = true, url = user.Role == Role.User ? Url.Action("Index", "Home") : Url.Action("Admin", "Home") });
        }

        [HttpGet]
        public JsonResult GetUser() => Json(new { user = this.Session["CurrentUser"] }, JsonRequestBehavior.AllowGet);

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        public ActionResult Index() => View();
    }
}