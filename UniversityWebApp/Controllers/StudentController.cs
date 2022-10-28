using BL.Services;
using DAL.Models;
using DAL.Repositories;
using System.Web.Mvc;

namespace UniversityWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) => _studentService = studentService;

        [HttpPost]
        public JsonResult CreateStudent(Student student)
        {
            var loggedUser = Session["CurrentUser"] as User;
            student.UserId = loggedUser.UserId;
            int studentId = _studentService.RegisterStudent(student);
            return Json(new { result = studentId > 0, studentId });
        }
    }
}