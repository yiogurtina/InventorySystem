using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.Controllers
{
    public class OrderEmployeesController : Controller
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderEmployeesController(ApplicationDbContext context, UserManager<Employee> userManager)
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
                if (await _userManager.IsInRoleAsync(usr, "Manager"))
                {
                    if (usr.Id == userId)
                    {
                        var applicationDbContext = _context.OrderEmployees
                            .Where(u => u.EmployeeFromId == usr.Id)
                            .Include(o => o.EmployeeFrom)
                            .Include(o => o.EmployeeTo)
                            .Include(o => o.Office);

                        return View(await applicationDbContext.ToListAsync());
                    }

                }
                else
                {
                    if (await _userManager.IsInRoleAsync(usr, "Admin"))
                    {
                        if (usr.Id == userId)
                        {
                            var applicationDbContext = _context.OrderEmployees
                                .Where(u => u.EmployeeFromId == usr.Id)
                                .Include(o => o.EmployeeFrom)
                                .Include(o => o.EmployeeTo)
                                .Include(o => o.Office);

                            return View(await applicationDbContext.ToListAsync());
                        }

                    }
                }

            }
            #endregion

            
            var applicationDbContextAll = _context.OrderEmployees
                .Include(o => o.EmployeeFrom)
                .Include(o => o.EmployeeToId)
                .Include(o => o.Office);
            return View(await applicationDbContextAll.ToListAsync());
        }

        #endregion

        #region OrderSend
        //[Authorize(Roles = "User, Admin")]
        public ActionResult OrderSend(string officeId, string title, string content, string employeeFromId, string employeeToId)
        {

            var orderSend = new OrderEmployee
            {
                OfficeId = officeId,
                Title = title,
                Content = content,
                EmployeeFromId = employeeFromId,
                EmployeeToId = employeeToId,
                DateFrom = DateTime.Now,
                DateTo = null
            };
            _context.Add(orderSend);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Status

        public ActionResult OrderStatus(string idMessage)
        {
            var messageId = _context.OrderEmployees.SingleOrDefault(m => m.Id == idMessage);
            if (messageId != null && messageId.Status == "New")
            {

                messageId.Status = "Open";
              
                _context.Update(messageId);
                _context.SaveChanges();
            }


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Details

        // GET: OrderEmployees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEmployee = await _context.OrderEmployees
                .Include(o => o.EmployeeFrom)
                .Include(o => o.Office)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (orderEmployee == null)
            {
                return NotFound();
            }

            return View(orderEmployee);
        }
        

        #endregion

        #region Delete

        // GET: OrderEmployees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEmployee = await _context.OrderEmployees
                .Include(o => o.EmployeeFrom)
                .Include(o => o.Office)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (orderEmployee == null)
            {
                return NotFound();
            }

            return View(orderEmployee);
        }

        // POST: OrderEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderEmployee = await _context.OrderEmployees.SingleOrDefaultAsync(m => m.Id == id);
            _context.OrderEmployees.Remove(orderEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderEmployeeExists(string id)
        {
            return _context.OrderEmployees.Any(e => e.Id == id);
        }

        #endregion
    }
}
