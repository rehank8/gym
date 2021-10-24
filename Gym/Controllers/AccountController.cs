using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Gym.Controllers
{
    public class AccountController : Controller
    {
        gymContext _db = new gymContext();
        [HttpGet]
        public IActionResult Login()
        {
            return View("_Login");
        }

        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnURL = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult LoginSave(LoginModel loginModel)
        {
            if (!string.IsNullOrEmpty(loginModel.UserName) && string.IsNullOrEmpty(loginModel.Password))
            {
                return RedirectToAction("Login");
            }

            var user = _db.UserModel.FirstOrDefault(x => x.UserName == loginModel.UserName
                                               && x.Password == loginModel.Password);
            if (user != null)
            {

                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Role, user.Role==1 ? "Admin"
                                    : user.Role==2 ? "User" : "Guest")
                }, CookieAuthenticationDefaults.AuthenticationScheme);


                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                if (user.Role == 1)
                    return RedirectToAction("GetData", "Home");

                return RedirectToAction("DemoVideo", "Home");
            }

            return RedirectToAction("Login");

        }
    }
}