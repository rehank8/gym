using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Controllers
{
    [Authorize(Roles = "User")]
    public class CustomerController : Controller
    {
        private readonly gymContext _db;

        public CustomerController(gymContext gymContext)
        {
            _db = gymContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Videos()
        {
            var models = _db.Videos.OrderByDescending(x => x.Id).ToList();
            return View(models);
        }

        [HttpGet]
        public IActionResult ListPayments()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            var userPayments = _db.Payment.Where(x => x.UserId == userId)
                               .OrderByDescending(x => x.CreatedDate)
                                .ToList();
            return View(userPayments);
        }
    }
}
