using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.Controllers
{
    public class OrderEmployeeAdminsController : Controller
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderEmployeeAdminsController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Index

        // GET: OrderEmployees
        //[Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Index()
        {
            #region Search office Manager

            var userId = _userManager.GetUserId(User);

            var userFromOff = _context.Users.Where(u => u.IsDelete == false);
            foreach (var usr in userFromOff)
            {
                if (await _userManager.IsInRoleAsync(usr, "Admin"))
                {
                    if (usr.Id == userId)
                    {
                        var applicationDbContext = _context.OrderEmployeeAdmin
                            .Where(u => u.EmployeeFromAdminId == usr.Id)
                            .Include(o => o.EmployeeFromAdmin)
                            .Include(o => o.EmployeeToAdmin)
                            .Include(o => o.OfficeAdmin);

                        return View(await applicationDbContext.ToListAsync());
                    }

                }

            }
            #endregion

            var applicationDbContextAll = _context.OrderEmployeeAdmin
                .Include(o => o.EmployeeFromAdmin)
                .Include(o => o.EmployeeToAdmin)
                .Include(o => o.OfficeAdmin);
            return View(await applicationDbContextAll.ToListAsync());
        }

        #endregion

        #region Delete

        // GET: OrderEmployeeAdmins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEmployeeAdmin = await _context.OrderEmployeeAdmins
                .Include(o => o.EmployeeFromAdmin)
                .Include(o => o.EmployeeToAdmin)
                .Include(o => o.OfficeAdmin)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (orderEmployeeAdmin == null)
            {
                return NotFound();
            }

            return View(orderEmployeeAdmin);
        }

        // POST: OrderEmployeeAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderEmployeeAdmin = await _context.OrderEmployeeAdmins.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderEmployeeAdmins.Remove(orderEmployeeAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderEmployeeAdminExists(string id)
        {
            return _context.OrderEmployeeAdmins.Any(e => e.Id == id);
        }

        #endregion

    }
}
