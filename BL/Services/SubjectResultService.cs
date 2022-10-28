using DAL.Models;
using DAL.Repositories;

namespace BL.Services
{
    public class SubjectResultService
    {
        private readonly IRepository<SubjectResult> _subjectResultRepository;

        public SubjectResultService(IRepository<SubjectResult> subjectResultRepository) => _subjectResultRepository = subjectResultRepository;

        public int AddResults(SubjectResult subjectResult)
        {
            int rows = _subjectResultRepository.Create(subjectResult);
            return rows;
        }


    }
}