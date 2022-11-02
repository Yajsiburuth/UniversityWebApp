using BL.Services;
using DAL.Models;
using DAL.ViewModels;
using System.Web.Mvc;
using UniversityWebApp.Helper;

namespace UniversityWebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) => _studentService = studentService;

        public ActionResult Register() => View();

        public ActionResult StudentProfile() => View();

        [HttpPost]
        public JsonResult CreateStudent(StudentViewModel studentViewModel)
        {
            if(studentViewModel.NationalId != null)              
                if (_studentService.CheckDuplicateNationalId(studentViewModel.NationalId)) ModelState.AddModelError("NationalId", "This National Id has already been registered");
            if(studentViewModel.PhoneNumber != null)
                if (_studentService.CheckDuplicatePhone(studentViewModel.PhoneNumber)) ModelState.AddModelError("PhoneNumber", "This Phone Number has already been registered");

            if (!ModelState.IsValid) return Json(new { result = false, errors = ErrorHelper.ModelStateErrorsToDict(ModelState) });
            Student student = new Student();
            student.FirstName = studentViewModel.FirstName;
            student.LastName = studentViewModel.LastName; 
            student.PhoneNumber = studentViewModel.PhoneNumber;
            student.DateOfBirth = studentViewModel.DateOfBirth;
            student.GuardianName = studentViewModel.GuardianName;
            student.NationalId = studentViewModel.NationalId;
            var loggedUser = Session["CurrentUser"] as User;
            student.UserId = loggedUser.UserId;
            int studentId = _studentService.RegisterStudent(student);
            return Json(new { result = studentId > 0, studentId });
        }

        public JsonResult GetDetails()
        {
            Student student = _studentService.GetStudent(int.Parse(Session["CurrentUserId"].ToString()));
            this.Session["CurrentStudentId"] = student.StudentId;
            return Json(new { studentDetails = student }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatus()
        {
            int userId = (int) Session["CurrentUserId"];
            string status = _studentService.GetStatus(userId);
            return Json(new { status }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentsSummary() => Json(new { studentsSummary = _studentService.GetSummary() }, JsonRequestBehavior.AllowGet);
    }
}