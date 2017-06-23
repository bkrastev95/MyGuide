using System;
using MyGuide.Domain.Services;
using MyGuide.Models;
using MyGuide.Repository;


namespace MyGuide.Services
{
    public class UserService : IUserService
    {
        private UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public User GetUser(long id)
        {
            return userRepository.GetUser(id);
        }

        public User Login(LoginModel model)
        {
            return userRepository.LoginUser(model);
        }
    }
}
