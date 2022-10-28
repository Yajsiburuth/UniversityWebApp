using DAL.Models;

namespace BL.Services
{
    public interface IStudentService
    {
        int RegisterStudent(Student student);
    }
}