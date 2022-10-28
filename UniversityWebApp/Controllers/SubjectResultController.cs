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
            int rows = _subjectResultService.CreateResults(subjectResults);
            return Json(new { result = rows > 0, url = Url.Action("Index", "Home") });
        }
    }
}