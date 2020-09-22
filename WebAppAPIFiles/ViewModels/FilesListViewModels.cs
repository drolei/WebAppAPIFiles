using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPIFiles.Data.Models;

namespace WebAppAPIFiles.ViewModels
{
    public class FilesListViewModels
    {
        public IEnumerable<Files> GetAllFiles { get; set; }

    }
}
