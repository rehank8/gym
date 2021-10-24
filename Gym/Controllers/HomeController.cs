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

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Images()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Videos()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult FileUpload(IFormFile fileupload)
        {
            if (fileupload != null)
            {
                if (fileupload == null || fileupload.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + fileupload.FileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    fileupload.CopyTo(stream);
                }
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
