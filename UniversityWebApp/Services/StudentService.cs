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

        public int RegisterStudent(Student student)
        {
            int studentId = _studentRepository.Create(student);
            return studentId;
        }


    }
}