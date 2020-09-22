using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPIFiles.Data.Interfaces;
using WebAppAPIFiles.Data.Models;

namespace WebAppAPIFiles.Data.Repository
{
    public class FilesRepository : IFiles
    {
        private readonly AppDBContent _appDBContent;

        public FilesRepository(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }


        public IEnumerable<Files> AllFiles => _appDBContent.Files.Include(c => c.User);
    }
}
