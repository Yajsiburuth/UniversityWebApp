using System.Web;
using DAL.Models;
using DAL.Repositories;

namespace BL.Services
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