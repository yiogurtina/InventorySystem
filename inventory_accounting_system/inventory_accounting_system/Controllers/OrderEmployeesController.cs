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
                        #region Search office Admin

                        var userIdAdmin = _userManager.GetUserId(User);
                        var userNameAdmin = _userManager.GetUserName(User);

                        ViewData["UserIdAdmin"] = userNameAdmin;

                        var officeIdEmployeeAdmin = _context.Offices;

                        var userFromOffAdmin = _context.Users.Where(u => u.IsDelete == false);
                        foreach (var usrAdmin in userFromOffAdmin)
                        {
                            if (await _userManager.IsInRoleAsync(usrAdmin, "Manager") && usrAdmin.Id == userIdAdmin)
                            {
                                ViewData["EmployeeToIdAdmin"] = new SelectList(_context.Users.Where(u => u.Id == usrAdmin.Id), "Id", "Name");
                                var userOfficeIdAdmin = usrAdmin.OfficeId;

                                foreach (var office in officeIdEmployeeAdmin)
                                {
                                    if (userOfficeIdAdmin == office.Id)
                                    {
                                        foreach (var usrAdminOffice in userFromOffAdmin)
                                        {
                                            ViewData["OfficeIdAdmin"] = new SelectList(_context.Offices.Where(o => o.Id == usrAdmin.OfficeId), "Id", "Title");
                                            ViewData["OfficeIdTitleAdmin"] = office.Title;

                                            foreach (var admin in userFromOffAdmin)
                                            {
                                                if (await _userManager.IsInRoleAsync(admin, "Admin"))
                                                {
                                                    ViewData["EmployeeFromIdAdmin"] = new SelectList(_context.Users.Where(u => u.Id == admin.Id), "Id", "Name");

                                                    ViewData["EmployeeFromIdNameAdmin"] = admin.Name;
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                        }

                        #endregion

                        var applicationDbContext = _context.OrderEmployees
                            .Where(u => u.EmployeeFromId == usr.Id)
                            .Include(o => o.EmployeeFrom)
                            .Include(o => o.EmployeeTo)
                            .Include(o => o.Office);

                        return View(await applicationDbContext.ToListAsync());
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

        #region StatusOpen

        public ActionResult OrderStatusOpen(string idMessageOpen)
        {
            var messageId = _context.OrderEmployees.SingleOrDefault(m => m.Id == idMessageOpen);
            if (messageId != null && messageId.Status == "Open")
            {

                messageId.Status = "Open";

                _context.Update(messageId);
                _context.SaveChanges();
            }


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region OrderSendAdmin
        //[Authorize(Roles = "User, Admin")]
        public async Task<ActionResult> OrderSendAdmin(
            string officeIdAdmin,
            string titleAdmin,
            string contentAdmin,
            string employeeFromIdAdmin,
            string employeeToIdAdmin,
            string idMessageOpen,
            OrderEmployee orderStatus)
        {
            OrderStatusInprogress(idMessageOpen);

            var orderSend = new OrderEmployeeAdmin
            {
                OfficeAdminId = officeIdAdmin,
                TitleAdmin = titleAdmin,
                ContentAdmin = contentAdmin,
                EmployeeFromAdminId = employeeFromIdAdmin,
                EmployeeToAdminId = employeeToIdAdmin,
                DateFromAdmin = DateTime.Now,
                DateToAdmin = null
            };

            var msg = _context.OrderEmployees;
            foreach (var orderEmployee in msg)
            {
                if (orderEmployee.Id == idMessageOpen)
                {
                    string contentUsr = orderEmployee.Content;
                    orderSend.ContentUser = contentUsr;
                }
            }
            _context.Add(orderSend);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region OrderStatusInprogress

        public ActionResult OrderStatusInprogress(string idMessageOpen)
        {
            var messageId = _context.OrderEmployees.SingleOrDefault(m => m.Id == idMessageOpen);
            if (messageId != null && messageId.Status == "Open")
            {

                messageId.Status = "Inprogress";
                messageId.DateTo = DateTime.Now;

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
