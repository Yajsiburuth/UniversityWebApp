using DAL.Models;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface ISubjectResultRepository : IRepository<SubjectResult>
    {
        void CreateResults(SubjectResult subjectResult);
    }
}