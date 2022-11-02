using DAL.Models;
using DAL.Repositories;
using DAL.ViewModels;
using System.Collections.Generic;

namespace BL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository) => _studentRepository = studentRepository;

        public int RegisterStudent(Student student) => _studentRepository.Create(student);
        public Student GetStudent(int userId) => _studentRepository.Find(userId);
        public string GetStatus(int userId)
        {
            int status = _studentRepository.GetStatus(userId);
            if(status == -1) return "Not Registered";
            return ((Status)status).ToString();
        }
        public bool CheckDuplicateNationalId(string nationalId) => _studentRepository.CheckNationalId(nationalId);
        public bool CheckDuplicatePhone(string phoneNumber) => _studentRepository.CheckPhone(phoneNumber);
        public List<StudentSummary> GetSummary() => _studentRepository.GetSummary();

        public List<int> ApproveStudents(List<int> studentIds) => _studentRepository.ApproveStudents(studentIds);

    }
}