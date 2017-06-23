using System;
using MyGuide.Models;

namespace MyGuide.Repository
{
    public class UserRepository : BaseRepository
    {
        public User GetUser(long id)
        {
            return Query<User>("public.getuser", new { userid = id });
        }

        public User LoginUser(LoginModel model)
        {
            return Query<User>("public.loginuser", new { uname = model.Username, pass = model.Password });
        }
    }
}
