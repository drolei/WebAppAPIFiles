using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPIFiles.Data.Models;
using WebAppAPIFiles.ViewModels;

namespace WebAppAPIFiles.Data.Interfaces
{
   public interface IUser
    {
        IEnumerable<User> AllUsers { get; }

        Task CreateUser(RegisterViewModel user);

    }
}
