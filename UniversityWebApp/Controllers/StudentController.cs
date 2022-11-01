using BL.Services;
using DAL.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UniversityWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) => _studentService = studentService;

        [Authorize]
        [HttpGet]
        public ActionResult Register() => View();

        [HttpGet]
        public ActionResult StudentProfile() => View();

        [HttpPost]
        public JsonResult CreateStudent(Student student)
        {
            var loggedUser = Session["CurrentUser"] as User;
            student.UserId = loggedUser.UserId;
            int studentId = _studentService.RegisterStudent(student);
            return Json(new { result = studentId > 0, studentId });
        }

        [HttpGet]
        public JsonResult GetDetails()
        {
            Student student = _studentService.GetStudent(int.Parse(Session["CurrentUserId"].ToString()));
            this.Session["CurrentStudentId"] = student.StudentId;
            return Json(new { studentDetails = student }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStatus()
        {
            int userId = (int) Session["CurrentUserId"];
            string status = _studentService.GetStatus(userId);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentsSummary() => Json(new { studentsSummary = _studentService.GetSummary() }, JsonRequestBehavior.AllowGet);

        [HttpPost]
        public JsonResult ApproveStudents(List<int> studentIds)
        {
            List<int> approvedIds = new List<int>();
            approvedIds =_studentService.ApproveStudents(studentIds);
            return Json(new { });
        }
    }
}