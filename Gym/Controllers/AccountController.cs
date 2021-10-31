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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;

namespace Gym.Controllers
{
    public class AccountController : Controller
    {
        private readonly gymContext _db;
        private readonly IHttpContextAccessor _accessor;

        public AccountController(gymContext gymContext, IHttpContextAccessor accessor)
        {
            _db = gymContext;
            _accessor = accessor;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(new LoginModel());
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile()
        {
            ViewData["Title"] = "UserProfile";
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var user = await _db.UserModel.FindAsync(userId);

                return View(user);

            }
            return RedirectToAction("login");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserProfile(UserModel userModel)
        {
            ViewData["Title"] = "UserProfile";
            if (ModelState.IsValid)
            {
                _db.UserModel.Update(userModel);
                await _db.SaveChangesAsync();

                return RedirectToAction("Logout");
            }

            return View(userModel);
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
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Role, user.UserType==1 ? "Admin"
                                    : user.UserType==2 ? "User" : "Guest")},
                                    CookieAuthenticationDefaults.AuthenticationScheme);


                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    var ipAddress = Request.HttpContext.Connection.RemoteIpAddress
                                        ?? _accessor.HttpContext.Connection.RemoteIpAddress
                                                ?? HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;

                    var loginHistory = new LoginHistory
                    {
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        IPAddress = Convert.ToString(ipAddress),
                        CreatedDate = DateTime.Now,
                        UserId = user.Id,
                        UserName = user.UserName,
                        Id = 0
                    };

                    _db.LoginHistory.Add(loginHistory);
                    await _db.SaveChangesAsync();


                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else if (user.UserType == 1)
                        return RedirectToAction("Index", "Admin");
                    else if (user.UserType == 2 && user.IsActive == true)
                        return RedirectToAction("Index", "Customer");
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

            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress
                                       ?? _accessor.HttpContext.Connection.RemoteIpAddress
                                               ?? HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

            var loginHistory = new LoginHistory
            {
                Action = ControllerContext.ActionDescriptor.ActionName,
                IPAddress = Convert.ToString(ipAddress),
                CreatedDate = DateTime.Now,
                UserId = userId,
                UserName = User.Identity.Name,
                Id = 0
            };

            _db.LoginHistory.Add(loginHistory);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}