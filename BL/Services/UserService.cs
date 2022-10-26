using DAL.Models;
using DAL.Repositories;
using DAL.ViewModels;
using Helpers.Helper;

namespace BL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository user)
        {
            _userRepository = user;
        }

        public User Register(string email, string password)
        {
            HashingHelper hashingHelper = new HashingHelper();
            byte[] salt = hashingHelper.CreateSalt();
            byte[] passwordHash = hashingHelper.HashPassword(password, salt);
            User user = new User { Email = email, PasswordHash = passwordHash, Salt = salt, Role = Role.User};
            _userRepository.Create(user);
            return user;
        }

        public User Authenticate(LoginUserViewModel loginVm)
        {
            User user = GetUser(loginVm.Email);
            if (user != null)
            {
                HashingHelper hashingHelper = new HashingHelper();
                if (!hashingHelper.VerifyHash(loginVm.Password, user.Salt, user.PasswordHash))
                    return null;
            }
            return user;
        }

        public User GetUser(string email) => _userRepository.Find(email);

    }
}