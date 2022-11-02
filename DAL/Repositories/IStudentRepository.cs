using DAL.Models;
using DAL.ViewModels;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<int> ApproveStudents(List<int> studentIds);
        int GetStatus(int UserId);
        bool CheckNationalId(string nationalId);
        bool CheckPhone(string nationalId);
        List<StudentSummary> GetSummary();

    }
}
