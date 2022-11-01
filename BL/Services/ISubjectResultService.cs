using DAL.Models;

namespace BL.Services
{
    public interface ISubjectResultService
    {
        void CreateResults(SubjectResult subjectResult);
        SubjectResult GetSubjectResults(int studentId);
    }
}