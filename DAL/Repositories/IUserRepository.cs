using DAL.Models;
namespace DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Find(string email);
    }
}
