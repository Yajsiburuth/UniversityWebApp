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
        bool CheckDuplicateNationalId(string nationalId);
        bool CheckDuplicatePhone(string phoneNumber);
        List<StudentSummary> GetSummary();
        List<int> ApproveStudents(List<int> studentIds);
    }
}