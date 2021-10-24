using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Gym.Controllers
{
	public class LoginController : Controller
	{
		gymContext _db = new gymContext();
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult LoginSave(LoginModel loginModel)
		{
			var user = _db.UserModel.FirstOrDefault(x => x.UserName == loginModel.UserName
											   && x.Password == loginModel.Password);
			if (user != null)
			{
				//HttpContext.Session.SetInt32("loginUserRole", user.Role);
				//HttpContext.Session.SetString("loginUserName", user.UserName);

				if (user.Role == 1)
					return RedirectToAction("GetData", "Home");

				return RedirectToAction("DemoVideo", "Home");
			}

			return Login();


		}
	}
}