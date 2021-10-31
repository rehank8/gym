using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    public class CertificationsController : Controller
    {

        private readonly gymContext _db;

        public CertificationsController(gymContext gymContext)
        {
            _db = gymContext;
        }
        // GET: Certifications
        public ActionResult Index()
        {
            var data = _db.Certifications.ToList();
            //List<Certifications> data = GetCertificateData();

            ViewBag.CertificationsList = data;
            return View(data);
        }

        public ActionResult PrintCertificate(int id)
        {
             var data = _db.Certifications.ToList();
            //List<Certifications> data = GetCertificateData();

            return View(data.FirstOrDefault(x => x.Id == id));
        }

        private static List<Certifications> GetCertificateData()
        {
            var data = new List<Certifications>();
            var certificate = new Certifications()
            {
                Description = "Certificate Description 001",
                Id = 1,
                Location = "LA",
                Type = "Certificate Type",
                Username = "User Name 001"
            };
            data.Add(certificate);

            certificate = new Certifications()
            {
                Description = "Certificate Description 002",
                Id = 1,
                Location = "Chicango",
                Type = "Certificate Type",
                Username = "User Name 002"
            };
            data.Add(certificate);
            return data;
        }

        // GET: Certifications/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Certifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Certifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Certifications collection)
        {
            try
            {
                // TODO: Add insert logic here
                _db.Certifications.Add(collection);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Certifications/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _db.Certifications.FirstOrDefault(x => x.Id == id);
            return View(data);
        }

        // POST: Certifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Certifications collection)
        {
            try
            {
                if (id != 0)
                {
                    var data = _db.Certifications.Find(id);
                    data.Username = collection.Username;
                    data.Location = collection.Location;
                    data.Type = collection.Type;
                    data.Description = collection.Description;
                    _db.Certifications.Update(data);
                    _db.SaveChanges();
                }
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Certifications/Delete/5
        public ActionResult Delete(int id)
        {
            _db.Certifications.Remove(_db.Certifications.Find(id));
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Certifications/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}