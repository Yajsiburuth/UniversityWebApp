using DAL.Models;
using DAL.Repositories;

namespace BL.Services
{
    public class SubjectResultService : ISubjectResultService
    {
        private readonly IRepository<SubjectResult> _subjectResultRepository;
        public SubjectResultService(IRepository<SubjectResult> subjectResultRepository) => _subjectResultRepository = subjectResultRepository;

        public int CreateResults(SubjectResult subjectResult) => _subjectResultRepository.Create(subjectResult);
    }
}