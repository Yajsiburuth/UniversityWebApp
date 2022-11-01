using DAL.Models;
using DAL.Repositories;
using System.Runtime.InteropServices;

namespace BL.Services
{
    public class SubjectResultService : ISubjectResultService
    {
        private readonly ISubjectResultRepository _subjectResultRepository;
        public SubjectResultService(ISubjectResultRepository subjectResultRepository) => _subjectResultRepository = subjectResultRepository;

        public void CreateResults(SubjectResult subjectResult) => _subjectResultRepository.CreateResults(subjectResult);
        public SubjectResult GetSubjectResults(int studentId) => _subjectResultRepository.Find(studentId);
    }
}