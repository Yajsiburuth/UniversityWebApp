using BL.Services;
using System.Web.Mvc;

namespace UniversityWebApp.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService) => _subjectService = subjectService;

        [HttpGet]
        public JsonResult GetSubjects() => Json(new { result = true, subjectList = _subjectService.GetSubjects() }, JsonRequestBehavior.AllowGet);
    }
}