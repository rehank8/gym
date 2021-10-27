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
		public IActionResult LoginSave(UserModel loginModel)
		{
			if (!string.IsNullOrEmpty(loginModel.UserName) && string.IsNullOrEmpty(loginModel.Password))
			{
				ViewBag.LoginErrorMessage = "Inavalid Username or Password";
				//return View("Inavalid Username or Password");
				return RedirectToAction("Login");
			}

			var user = _db.UserModel.FirstOrDefault(x => x.UserName == loginModel.UserName
											   && x.Password == loginModel.Password);
 
			if (user != null)
			{

				var identity = new ClaimsIdentity(new[] {
					new Claim(ClaimTypes.Name, user.UserName),
					 new Claim(ClaimTypes.Role, user.UserType==1 ? "Admin"
									: user.UserType==2 ? "User" : "Guest")
				}, CookieAuthenticationDefaults.AuthenticationScheme);


				var principal = new ClaimsPrincipal(identity);

				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				CookieOptions options = new CookieOptions();
				options.Expires = DateTimeOffset.MaxValue;

				if (user.UserType == 1)
					return RedirectToAction("GetData", "Home");
				if (user.UserType == 2 && user.IsActive == true)
					return RedirectToAction("Video", "Home");
			}
			else
			{
				ViewBag.LoginErrorMessage = "Inavalid Username or Password";
			}

			//return RedirectToAction("Login");
			return RedirectToAction("Index","Home");

		}

		public IActionResult Logout()
		{
			Response.Cookies.Delete("LoginCookie");
			return RedirectToAction("/Home");
		}
	}
}