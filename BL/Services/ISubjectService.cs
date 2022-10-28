using DAL.Models;
using System.Collections.Generic;

namespace BL.Services
{
    public interface ISubjectService
    {
        IEnumerable<Subject> GetSubjects();
    }
}