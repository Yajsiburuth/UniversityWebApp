using DAL.Models;
using DAL.Repositories;

namespace BL.Services
{
    public class SubjectResultService : ISubjectResultService
    {
        private readonly ISubjectResultRepository _subjectResultRepository;
        public SubjectResultService(ISubjectResultRepository subjectResultRepository) => _subjectResultRepository = subjectResultRepository;

        public void CreateResults(SubjectResult subjectResult) => _subjectResultRepository.CreateResults(subjectResult);
    }
}