using DAL.Models;
using DAL.Repositories;

namespace BL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentService(IRepository<Student> studentRepository) => _studentRepository = studentRepository;

        public int RegisterStudent(Student student) => _studentRepository.Create(student);
    }
}