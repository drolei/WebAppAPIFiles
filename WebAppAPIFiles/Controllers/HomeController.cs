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
            ViewBag.Title = "start page";
            ViewBag.Login = User.Identity.Name;
            
            var currentUserID = User.Identity.Name;
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
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняю файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
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
            // Путь к файлу
            string file_path = Path.Combine("~", file.Path);
            // Тип файла - content-type
            string file_type = "application/zip";
            
            return File(file_path, file_type);
        }



    }
}
