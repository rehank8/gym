using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gym.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace Gym.Controllers
{
	public class HomeController : Controller
	{
		private readonly gymContext _db;

		public HomeController(gymContext gymContext)
		{
			_db = gymContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		//[Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IActionResult DemoImages()
        {
            var models = _db.Images.OrderByDescending(x => x.Id).ToList();
            return View(models);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IActionResult DemoVideo()
        {
            var models = _db.Videos.OrderByDescending(x => x.Id).ToList();
            return View(models);
        }

		[Authorize(Roles = "User")]
		[HttpGet]
		public IActionResult Video()
		{
			return View();
		}

		//[HttpPost]
		//public IActionResult Login()
		//{
		//	return RedirectToAction(nameof(GetData));
		//}

		
		[HttpPost]
		public IActionResult Index(Appointments appointment)
		{
			if (ModelState.IsValid)
			{
				_db.Appointments.Add(appointment);
				_db.SaveChanges();
				ModelState.Clear();
			}

			return View();
		}

		

		public IActionResult HomePage()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Pricing()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		


		
	}
}
