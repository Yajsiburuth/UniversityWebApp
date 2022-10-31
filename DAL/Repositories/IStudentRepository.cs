using DAL.Models;
using DAL.ViewModels;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<int> ApproveStudents(List<int> studentIds);
        int GetStatus(int UserId);
        List<StudentSummary> GetSummary();

    }
}
