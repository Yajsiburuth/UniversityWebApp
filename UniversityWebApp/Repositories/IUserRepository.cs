using UniversityWebApp.Models;
namespace UniversityWebApp.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Find(string email);
    }
}
