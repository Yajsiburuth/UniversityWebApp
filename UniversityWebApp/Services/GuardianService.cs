using UniversityWebApp.Models;
using UniversityWebApp.Repositories;

namespace UniversityWebApp.Services
{
    public class GuardianService
    {

        private readonly IRepository<Guardian> _guardianRepository;

        public GuardianService(IRepository<Guardian> guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }

        public int CreateGuardian(Guardian guardian)
        {
            int guardianId = _guardianRepository.Create(guardian);
            return guardianId;
        }


    }
}