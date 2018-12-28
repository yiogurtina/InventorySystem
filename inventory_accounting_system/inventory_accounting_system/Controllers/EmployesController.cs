using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace inventory_accounting_system.Controllers
{
    public class EmployesController : Controller
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;

        private readonly UserManager<Employee> _userManager;

        public EmployesController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        #endregion

        #region Index

        // GET: Employes
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Details

        // GET: Employes/Details/5
        public ActionResult Details(string id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        #endregion

        #region Create

        // GET: Employes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employes/Create
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

        #endregion

        #region Edit

        // GET: Employes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employes/Edit/5
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

        #endregion

        #region Delete

        // GET: Employes/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewData["Name"] = user.Name;
            ViewData["Surname"] = user.Surname;
            ViewData["UserId"] = id;
            return View();
        }

        // POST: Employes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                var user = await _userManager.FindByIdAsync(id);
                user.IsDelete = true;

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Users));
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region ChangeRole

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = _context.Roles;
            ViewData["Roles"] = new SelectList(roles);
            ViewData["UserId"] = id;
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Users()
        {
            var user = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Admin"))
            {
                return View(_context.Users.Where(u => u.Id != user.Id).Where(u => !u.IsDelete));
            }
            else
            {
                return View(_context.Users.Where(u => u.OfficeId == user.OfficeId).Where(u=>u.Id!=user.Id).Where(u => !u.IsDelete));
            }
        }

        public async Task<IActionResult> UserAssets(string id)
        {
            var user = _context.Users.Include(u=>u.Assets).First(u=>u.Id==id );
            var assets = user.Assets;
            return View(assets);
        }

        #endregion

        #region CreateRole

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string role, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            List<string> allRoles = new List<string>()
            {
                "Admin",
                "Manager",
                "User"
            };                        
            await _userManager.RemoveFromRolesAsync(user, allRoles);
            await _userManager.AddToRoleAsync(user, role);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}