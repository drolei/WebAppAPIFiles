using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPIFiles.Data.Interfaces;
using WebAppAPIFiles.Data.Models;
using WebAppAPIFiles.ViewModels;

namespace WebAppAPIFiles.Data.Repository
{
    public class UserRepository : IUser
    {
        private readonly AppDBContent _appDBContent;

        public UserRepository(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }



        public IEnumerable<User> AllUsers => _appDBContent.User;

        public async Task CreateUser(RegisterViewModel user)
        {
            _appDBContent.User.Add(new User
            {
                GivenName = user.GivenName,
                MiddleName =user.MiddleName,
                Surname = user.Surname,
                Email = user.Email,
                PasswordHash = user.Password,
                Phone = user.Phone,
            });

            await _appDBContent.SaveChangesAsync();
        }
    }
}
