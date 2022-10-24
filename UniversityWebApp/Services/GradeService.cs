using UniversityWebApp.Models;
using UniversityWebApp.Repositories;

namespace UniversityWebApp.Services
{
    public class GradeService
    {

        private readonly IRepository<Grade> _gradeRepository;

        public GradeService(IRepository<Grade> gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public int AddResults(Grade grade)
        {
            int rows = _gradeRepository.Create(grade);
            return rows;
        }


    }
}