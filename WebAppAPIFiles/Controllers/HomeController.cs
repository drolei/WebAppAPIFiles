using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppAPIFiles.Data;
using WebAppAPIFiles.Data.Interfaces;
using WebAppAPIFiles.Data.Models;
using WebAppAPIFiles.ViewModels;

namespace WebAppAPIFiles.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFiles  _ifiles;
        private readonly IUser _iuser;
        private readonly AppDBContent _appDBContent;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(IFiles files, IUser iuser, AppDBContent appDBContent, IWebHostEnvironment appEnvironment)
        {
            _ifiles = files;
            _iuser = iuser;
            _appDBContent = appDBContent;
            _appEnvironment = appEnvironment;

        }

        public IActionResult Index()
        {
            var currentUserID = User.Identity.Name;
            ViewBag.Title = "start page";
            ViewBag.Login = _iuser.AllUsers.First(i => i.Email == currentUserID).GivenName;
            
            
            IEnumerable<Files> files = null;

            files = _ifiles.AllFiles.Where(i => i.User.Email == currentUserID );

            var fileObj = new FilesListViewModels
            {
                GetAllFiles = files
            };





            return View(fileObj);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            var currentUserid = _iuser.AllUsers.First(i => i.Email == User.Identity.Name);
            if (uploadedFile != null)
            {
                string path = _appEnvironment.WebRootPath + $"/Files/{currentUserid.Email}/";
                Directory.CreateDirectory(path);

                // The path to the folder Files
                path += uploadedFile.FileName;
                // I save the file to a folder Files in the catalog wwwroot
                using (var fileStream = new FileStream( path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                Files file = new Files { Name = uploadedFile.FileName, Path = path, UserId = currentUserid.Id  };
                _appDBContent.Files.Add(file);
                _appDBContent.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public VirtualFileResult GetVirtualFile(Guid id)
        {
            var file = _ifiles.AllFiles.First(i => i.Id == id);
            // Path to the file
            string file_path = Path.Combine("~", file.Path);
            // Type file - content-type
            string file_type = "application/zip";
            
            return File(file_path, file_type);
        }



    }
}
