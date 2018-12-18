using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace inventory_accounting_system.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ApplicationDbContext _context;

        public RolesController(UserManager<Employee> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var roles = _context.Roles;

            if (roles.Count() == 0)
            {
                var adminRole = new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN"};
                var userRole = new IdentityRole() { Name = "User", NormalizedName = "USER"};
                var managerRole = new IdentityRole() { Name = "Manager" , NormalizedName = "MANAGER"};
                _context.Add(adminRole);
                _context.Add(userRole);
                _context.Add(managerRole);
                await _context.SaveChangesAsync();
                var adminUser = new Employee { Login = "admin", UserName = "admin" };
                await _userManager.CreateAsync(adminUser, "admin");

                var addToRole = new IdentityUserRole<string>() { UserId = adminUser.Id, RoleId = adminRole.Id };
                _context.Add(addToRole);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Roles/Delete/5
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