using DAL.Models;
using DAL.Repositories;

namespace BL.Services
{
    public class GuardianService
    {

        private readonly IGuardianRepository _guardianRepository;

        public GuardianService(IGuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }

        public int CreateGuardian(Guardian guardian)
        {
            int guardianId;
            guardianId = _guardianRepository.Find(guardian.PhoneNumber);
            if(guardianId < 1)
                guardianId = _guardianRepository.Create(guardian);

            return guardianId;
        }


    }
}