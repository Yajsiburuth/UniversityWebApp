using DAL.Models;
using DAL.ViewModels;
using System.Collections.Generic;

namespace BL.Services
{
    public interface IStudentService
    {
        int RegisterStudent(Student student);
        string GetStatus(int userId);
        Student GetStudent(int userId);
        List<StudentSummary> GetSummary();
        List<int> ApproveStudents(List<int> studentIds);
    }
}