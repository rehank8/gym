using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gym.Models;

namespace Gym.Controllers
{
	public class HomeController : Controller
	{

		gymContext _db = new gymContext();

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login()
		{
			return RedirectToAction(nameof(GetData));
		}

		[HttpGet]
		public IActionResult GetData()
		{
			return View(_db.Appointments.ToList());
		}

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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
