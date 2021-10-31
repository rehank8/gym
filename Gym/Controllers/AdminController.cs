using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly gymContext _db;

        public AdminController(gymContext gymContext)
        {
            _db = gymContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Admin Dashboard";
            return View();
        }

        [HttpGet]
		public IActionResult Appointments()
		{
			return View(_db.Appointments.ToList());
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
        public IActionResult EditCustomer(Appointments appointment)
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
            return RedirectToAction("GetData", "Admin");
        }

        [HttpGet]
		public IActionResult Users()
		{
			return View(_db.UserModel.ToList());
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

        [HttpPost]
        public IActionResult EditUser(UserModel user)
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
            return RedirectToAction("GetUsers","Admin");
        }

        [HttpGet]
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
            return RedirectToAction("GetUsers", "Admin");
        }

        [HttpGet]
        public IActionResult Images()
        {
            var imageModels = _db.Images.OrderByDescending(x => x.Id).ToList();

            return View(imageModels);
        }

        [HttpGet]
        public IActionResult Videos()
        {
            var models = _db.Videos.OrderByDescending(x => x.Id).ToList();
            return View(models);
        }

        [HttpPost]
        public ActionResult DeleteImage(IFormCollection frmCollection)
        {

            var imageId = int.Parse(frmCollection["hdnImageId"]);
            var image = _db.Images.FirstOrDefault(x => x.Id == imageId);
            _db.Images.Remove(image);
            _db.SaveChanges();

            return RedirectToAction("Images", "Admin");
        }

        [HttpPost]
        public ActionResult DeleteVideo(IFormCollection frmCollection)
        {

            var id = int.Parse(frmCollection["hdnVideoId"]);
            var video = _db.Videos.FirstOrDefault(x => x.Id == id);
            _db.Videos.Remove(video);
            _db.SaveChanges();

            return RedirectToAction("Videos", "Admin");
        }

        [HttpPost]
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

            return RedirectToAction("Images", "Admin");
        }

        [HttpPost]
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

            return RedirectToAction("Videos", "Admin");
        }

        [HttpGet]
        public IActionResult LoginHistory()
        {
            return View(_db.LoginHistory.OrderByDescending(x=>x.CreatedDate).ToList());
        }
    }
}
