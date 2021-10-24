using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gym.Models;
using Microsoft.AspNetCore.Http;

namespace Gym.Controllers
{
	public class HomeController : Controller
	{

		gymContext _db = new gymContext();

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult DemoVideo()
		{
			return View();
		}

		//[HttpPost]
		//public IActionResult Login()
		//{
		//	return RedirectToAction(nameof(GetData));
		//}

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

		[HttpGet]
		public IActionResult EditCustomer(long? id)
		{
			Appointments model = new Appointments();
			if (id.HasValue)
			{
				Appointments customer = _db.Set<Appointments>().SingleOrDefault(c => c.Id == id.Value);
				if (customer != null)
				{
					model.Id = customer.Id;
					model.Name = customer.Name;
					model.Phone = customer.Phone;
					model.Email = customer.Email;
					model.Date = customer.Date;
				}
			}
			return View(model);
		}
	}
}
