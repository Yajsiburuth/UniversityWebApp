using System.Web;
using UniversityWebApp.Models;
using UniversityWebApp.Repositories;

namespace UniversityWebApp.Services
{
    public class StudentService
    {

        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student RegisterStudent(Student student)
        {
            int currentUserId = int.Parse(HttpContext.Current.User.Identity.Name);
            student.UserId = currentUserId;
            _studentRepository.Create(student);
            return student;
        }


    }
}