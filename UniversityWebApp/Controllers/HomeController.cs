using System.Web.Mvc;
using UniversityWebApp.Repositories;
using UniversityWebApp.Services;
using UniversityWebApp.ViewModels;

namespace UniversityWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SubjectService _subjectService = new SubjectService(new SubjectRepository());

        [Authorize]
        [HttpGet]
        public ActionResult RegisterStudent()
        {
            return View(new StudentViewModel()
            {
                subjectList = _subjectService.GetSubjects()
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult RegisterStudent(StudentViewModel studentVm)
        {
            return View();

        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}