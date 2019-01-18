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
        #region Dependency Injection

        private readonly SignInManager<Employee> _signInManager;

        public HomeController(SignInManager<Employee> signInManager)
        {
            _signInManager = signInManager;
        }

        #endregion

        #region Index

        [Authorize]
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {                
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region About

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        #endregion

        #region Contact

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        #endregion

        #region Error

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Chat

        public IActionResult Chat()
        {
            return View();
        }

        #endregion
    }
}
