using MyGuide.Models;

namespace MyGuide.Domain.Services
{
    public interface IUserService
    {
        User Login(LoginModel model);
    }
}
