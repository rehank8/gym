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
        private readonly gymContext _db;
        public AccountController(gymContext gymContext)
        {
            _db = gymContext;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            return View(new LoginModel());
        }

        [HttpGet]
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnURL = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = _db.UserModel.FirstOrDefault(x => x.UserName == loginModel.UserName
                                                   && x.Password == loginModel.Password);

                if (user != null)
                {

                    if (!user.IsActive)
                    {
                        ModelState.AddModelError("inactive", "User is inactive");
                        return View(loginModel);
                    }

                    var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Role, user.UserType==1 ? "Admin"
                                    : user.UserType==2 ? "User" : "Guest")},
                                    CookieAuthenticationDefaults.AuthenticationScheme);


                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else  if (user.UserType == 1)
                        return RedirectToAction("GetData", "Home");
                    else if (user.UserType == 2 && user.IsActive == true)
                        return RedirectToAction("Video", "Home");
                }
                else
                {
                    ModelState.AddModelError("invaliduser", "UserName or Password invalid.");
                    return View(loginModel);
                }
            }
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}