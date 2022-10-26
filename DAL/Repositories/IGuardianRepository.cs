using DAL.Models;

namespace DAL.Repositories
{
    public interface IGuardianRepository : IRepository<Guardian>
    {
        int Find(string phoneNumber);
    }
}