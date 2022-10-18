using System;
using UniversityWebApp.Helper;
using UniversityWebApp.Models;
using UniversityWebApp.Repositories;

namespace UniversityWebApp.Services
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

        public User Authenticate(string email, string password)
        {
            User user = GetUser(email);
            if (user != null)
            {
                HashingHelper hashingHelper = new HashingHelper();
                if (!hashingHelper.VerifyHash(password, user.Salt, user.PasswordHash))
                    return null;
            }
            return user;
        }

        public User GetUser(string email) => _userRepository.Find(email);

    }
}