﻿using System;
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

        [Authorize(Roles = "Admin, User")]
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

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult GetData()
		{
			var u = this.HttpContext.User as System.Security.Claims.ClaimsPrincipal;
			var isAdmin = u.Claims.Any(c => c.Value == "admin");
			ViewBag.UserType = isAdmin;
			return View(_db.Appointments.ToList());
		}
		[HttpGet]
		public IActionResult GetUsers()
		{
			var u = this.HttpContext.User as System.Security.Claims.ClaimsPrincipal;
			var isAdmin = u.Claims.Any(c => c.Value == "admin");
			ViewBag.UserType = isAdmin;
			return View(_db.UserModel.ToList());
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

		[HttpGet]
		public IActionResult EditUser(long? id)
		{
			UserModel model = new UserModel();
			if (id.HasValue)
			{
				UserModel user = _db.UserModel.FirstOrDefault(c => c.Id == id.Value);
				if (user != null)
				{
					model.Id = user.Id;
					model.UserName = user.UserName;
					model.Password = user.Password;
					model.UserType = user.UserType;
					model.IsActive = user.IsActive;
				}
			}
			return View(model);
		}

		public IActionResult Create()
		{
			
			var user = new UserModel();
			user.UserType = 2;
			return View(user);
		}

		[HttpPost]
		public IActionResult CreateUser(UserModel user)
		{
			if (ModelState.IsValid)
			{
				_db.UserModel.Add(user);
				_db.SaveChanges();
				ModelState.Clear();
			}
			return RedirectToAction("GetUsers");
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

		[HttpGet]
		public IActionResult EditCustomer(long? id)
		{
			Appointments model = new Appointments();
			if (id.HasValue)
			{
				Appointments customer = _db.Appointments.FirstOrDefault(c => c.Id == id.Value);
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

		[HttpPost]
		public IActionResult EditCustomerSave(Appointments appointment)
		{
			Appointments model = new Appointments();
			if (appointment != null)
			{
				Appointments customer = _db.Appointments.FirstOrDefault(c => c.Id == appointment.Id);
				if (customer != null)
				{
					customer.Name = appointment.Name;
					customer.Phone = appointment.Phone;
					customer.Email = appointment.Email;
					customer.Date = appointment.Date;

				}
				_db.Appointments.Update(customer);
				_db.SaveChanges();
			}
			return RedirectToAction("GetData");
		}


		[HttpPost]
		public IActionResult EditUserSave(UserModel user)
		{
			if (user != null)
			{
				UserModel userModel = _db.UserModel.FirstOrDefault(c => c.Id == user.Id);
				if (userModel != null)
				{
					userModel.UserType = user.UserType;
					userModel.UserName = user.UserName;
					userModel.Password = user.Password;
					userModel.IsActive = user.IsActive;

				}
				_db.UserModel.Update(userModel);
				_db.SaveChanges();
			}
			return RedirectToAction("GetUsers");
		}


		[Authorize(Roles = "Admin")]
		public IActionResult Images()
		{
			var imageModels = _db.Images.OrderByDescending(x => x.Id).ToList();

			return View(imageModels);
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Videos()
        {
            var models = _db.Videos.OrderByDescending(x => x.Id).ToList();
            return View(models);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteImage(IFormCollection frmCollection)
        {

            var imageId = int.Parse(frmCollection["hdnImageId"]);
            var image = _db.Images.FirstOrDefault(x => x.Id == imageId);
            _db.Images.Remove(image);
            _db.SaveChanges();

            return RedirectToAction("Images");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteVideo(IFormCollection frmCollection)
        {

            var id = int.Parse(frmCollection["hdnVideoId"]);
            var video = _db.Videos.FirstOrDefault(x => x.Id == id);
            _db.Videos.Remove(video);
            _db.SaveChanges();

            return RedirectToAction("Videos");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ImageUpload(IFormCollection frmCollection, IFormFile fileupload)
        {
            if (fileupload != null)
            {
                if (fileupload == null || fileupload.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "images");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + fileupload.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    fileupload.CopyTo(stream);
                }

                Images image = new Images()
                {
                    imagedescription = Convert.ToString(frmCollection["txtDescription"]),
                    imagepath = fileupload.FileName
                };

                _db.Images.Add(image);
                _db.SaveChanges();
            }

            return RedirectToAction("Images");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult VideoUpload(IFormCollection frmCollection, IFormFile fileupload)
        {
            if (fileupload != null)
            {
                if (fileupload == null || fileupload.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "videos");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + fileupload.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    fileupload.CopyTo(stream);
                }

                Videos video = new Videos()
                {
                    videodescription = Convert.ToString(frmCollection["txtDescription"]),
                    videopath = fileupload.FileName
                };

                _db.Videos.Add(video);
                _db.SaveChanges();
            }

			return RedirectToAction("Videos");
		}

		[Authorize(Roles = "Admin")]
		public IActionResult GetCertification()
		{
			return View();
		}
	}
}
