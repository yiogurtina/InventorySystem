using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace inventory_accounting_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<Employee> _signInManager;

        public HomeController(SignInManager<Employee> signInManager)
        {
            _signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
