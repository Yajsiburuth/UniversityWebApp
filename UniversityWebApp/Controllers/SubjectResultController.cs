using BL.Services;
using DAL.Models;
using DAL.Repositories;
using System.Web.Mvc;

namespace UniversityWebApp.Controllers
{
    public class SubjectResultController : Controller
    {
        private readonly ISubjectResultService _subjectResultService;
        public SubjectResultController(ISubjectResultService subjectResultService) => _subjectResultService = subjectResultService;

        [HttpPost]
        public JsonResult CreateResults(SubjectResult subjectResults)
        {
            _subjectResultService.CreateResults(subjectResults);
            return Json(new { result = true, url = Url.Action("Index", "Home") });
        }

        public JsonResult GetResults() => Json(new { resultDetails = _subjectResultService.GetSubjectResults(int.Parse(Session["CurrentStudentId"].ToString())) }, JsonRequestBehavior.AllowGet);
    }
}