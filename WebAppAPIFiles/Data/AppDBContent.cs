using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPIFiles.Data.Models;

namespace WebAppAPIFiles.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

        }

        public DbSet<Files> Files { get; set; }

        public DbSet<User> User { get; set; }


    }
}
