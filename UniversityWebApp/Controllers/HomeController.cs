﻿using System.Web.Mvc;
using DAL.Models;
using DAL.Repositories;
using BL.Services;

namespace UniversityWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SubjectService _subjectService = new SubjectService(new SubjectRepository());
        private readonly StudentService _studentService = new StudentService(new StudentRepository());
        private readonly SubjectResultService _gradeService = new SubjectResultService(new SubjectResultRepository());

        [Authorize]
        [HttpGet]
        public ActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateStudent(Student student)
        {
            var loggedUser = Session["CurrentUser"] as User;
            student.UserId = loggedUser.UserId;
            int studentId = _studentService.RegisterStudent(student);
            return Json( new { result = studentId > 0 ? true : false, studentId });
        }

        [HttpPost]
        public JsonResult AddResults(SubjectResult subjectResults)
        {
            int rows = _gradeService.AddResults(subjectResults);
            return Json(new { result = rows > 0 ? true : false, url = Url.Action("Index", "Home") });
        }

        public JsonResult GetSubjects()
        {
            var subjectList = _subjectService.GetSubjects();
            return Json(new { result = true, subjectList }, JsonRequestBehavior.AllowGet);
        }

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