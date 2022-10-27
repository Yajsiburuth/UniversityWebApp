using DAL.Models;
using DAL.Repositories;

namespace BL.Services
{
    public class GradeService
    {
        private readonly IRepository<Grade> _gradeRepository;

        public GradeService(IRepository<Grade> gradeRepository) => _gradeRepository = gradeRepository;

        public int AddResults(Grade grade)
        {
            int rows = _gradeRepository.Create(grade);
            return rows;
        }


    }
}