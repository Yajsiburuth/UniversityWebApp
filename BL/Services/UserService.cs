using DAL.Models;
using DAL.Repositories;
using DAL.ViewModels;
using Helpers.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository user) => _userRepository = user;

        public User Register(string email, string password)
        {
            User user = _userRepository.Find(email);
            if (user != null) return null;
            byte[] salt = HashingHelper.CreateSalt();
            byte[] passwordHash = HashingHelper.HashPassword(password, salt);
            user = new User { Email = email, PasswordHash = passwordHash, Salt = salt, Role = Role.User };
            _userRepository.Create(user);
            return user;
        }

        public User Authenticate(LoginUserViewModel loginUserViewModel)
        {
            User user = _userRepository.Find(loginUserViewModel.Email);
            if (user == null) return null;
            if (!HashingHelper.VerifyHash(loginUserViewModel.Password, user.Salt, user.PasswordHash)) return null;
            return user;
        }
    }
}