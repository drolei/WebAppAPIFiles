using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppAPIFiles.Data.Interfaces;
using WebAppAPIFiles.Data.Models;
using WebAppAPIFiles.ViewModels;

namespace WebAppAPIFiles.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUser _iuser;
        public AccountController(IUser iuser)
        {
            _iuser = iuser;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _iuser.AllUsers.FirstOrDefault(u => u.Email == model.EmailOrPhoneNumber && u.PasswordHash == model.Password ||  u.Phone == model.EmailOrPhoneNumber && u.PasswordHash == model.Password);
                if (user != null)
                {
                    await Authenticate(user.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                
                

            }
            ModelState.AddModelError("", "Invalid Email or Phone and(or) password");
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _iuser.AllUsers.FirstOrDefault(u => u.Email == model.Email && u.Phone == model.Phone);
                if (user == null)
                {
                    // добавляю пользователя в бд
                    await _iuser.CreateUser(model);

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные Email or Phone и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
