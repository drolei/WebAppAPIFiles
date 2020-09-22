using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAPIFiles.Data.Models;

namespace WebAppAPIFiles.Data.Interfaces
{
   public interface IFiles
    {
        IEnumerable<Files> AllFiles { get; }



    }
}
