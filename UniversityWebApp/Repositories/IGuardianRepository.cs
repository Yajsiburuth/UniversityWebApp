using System.Collections.Generic;
using UniversityWebApp.Models;

namespace UniversityWebApp.Repositories
{
    public interface IGuardianRepository : IRepository<Guardian>
    {
        int Find(string phoneNumber);
    }
}