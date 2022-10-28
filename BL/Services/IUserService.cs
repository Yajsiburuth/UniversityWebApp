using DAL.Models;
using DAL.ViewModels;

namespace BL.Services
{
    public interface IUserService
    {
        User Authenticate(LoginUserViewModel loginUserViewModel);
        User Register(string email, string password);
    }
}