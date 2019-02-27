using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace inventory_accounting_system.Controllers {
    public class OrderEmployeeAdminsController : Controller {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderEmployeeAdminsController (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Index

        // GET: OrderEmployees
        //[Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Index () {
            #region Search office Manager

            var userId = _userManager.GetUserId (User);

            var userFromOff = _context.Users.Where (u => u.IsDelete == false);
            foreach (var usr in userFromOff) {
                if (await _userManager.IsInRoleAsync (usr, "Admin")) {
                    if (usr.Id == userId) {
                        var applicationDbContext = _context.OrderEmployeeAdmins
                            .Where (u => u.EmployeeFromAdminId == usr.Id)
                            .Include (o => o.EmployeeFromAdmin)
                            .Include (o => o.EmployeeToAdmin)
                            .Include (o => o.OfficeAdmin);

                        return View (await applicationDbContext.ToListAsync ());
                    }

                }

            }
            #endregion

            var applicationDbContextAll = _context.OrderEmployeeAdmins
                .Include (o => o.EmployeeFromAdmin)
                .Include (o => o.EmployeeToAdmin)
                .Include (o => o.OfficeAdmin);
            return View (await applicationDbContextAll.ToListAsync ());
        }

        #endregion

        #region Delete

        // GET: OrderEmployeeAdmins/Delete/5
        public async Task<IActionResult> Delete (string id) {
            if (id == null) {
                return NotFound ();
            }

            var orderEmployeeAdmin = await _context.OrderEmployeeAdmins
                .Include (o => o.EmployeeFromAdmin)
                .Include (o => o.EmployeeToAdmin)
                .Include (o => o.OfficeAdmin)
                .SingleOrDefaultAsync (m => m.Id == id);
            if (orderEmployeeAdmin == null) {
                return NotFound ();
            }

            return View (orderEmployeeAdmin);
        }

        // POST: OrderEmployeeAdmins/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (string id) {
            var orderEmployeeAdmin = await _context.OrderEmployeeAdmins.SingleOrDefaultAsync (m => m.Id == id);
            _context.OrderEmployeeAdmins.Remove (orderEmployeeAdmin);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool OrderEmployeeAdminExists (string id) {
            return _context.OrderEmployeeAdmins.Any (e => e.Id == id);
        }

        #endregion

        #region OrderSendAdmin
        //[Authorize(Roles = "User, Admin")]
        public ActionResult OrderSendAdmin (string officeId, string title, string content, string employeeFromId, string employeeToId) {

            var orderSend = new OrderEmployee {
                OfficeId = officeId,
                Title = title,
                Content = content,
                EmployeeFromId = employeeFromId,
                EmployeeToId = employeeToId,
                DateFrom = DateTime.Now,
                DateTo = null
            };
            _context.Add (orderSend);
            _context.SaveChanges ();

            return RedirectToAction (nameof (Index));
        }

        #endregion

        #region OrderSendAdminOrder
        //[Authorize(Roles = "User, Admin")]
        public ActionResult OrderSendAdminOrder(string[] checkedCheckBoxOrderOpen)
        {

            foreach (var item in checkedCheckBoxOrderOpen)
            {

                var msgEmployee = _context.OrderEmployees.Where(a => a.EmployeeFromId == item);
                foreach (var msg in msgEmployee)
                {

                    // OrderStatusInprogress(msg.Id);

                    var orderSendAdmin = new OrderEmployeeAdmin
                    { // To в : From из
                        AssetId = msg.AssetId,
                        ContentAdmin = "Заявка не преобретение имущества",
                        DateToAdmin = DateTime.Now,
                        EmployeeFromAdminId = msg.EmployeeToId,
                        OfficeAdminId = msg.OfficeId,
                    };
                    _context.Add(orderSendAdmin);
                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

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

        #region StatusAdmin

        public ActionResult OrderStatusAdmin (string idMessage, OrderEmployee orderEmployee) {
            var messageId = _context.OrderEmployeeAdmins.SingleOrDefault (m => m.Id == idMessage);
            if (messageId != null && messageId.StatusAdmin == "New") {

                messageId.StatusAdmin = "Open";
                _context.Update (messageId);
                _context.SaveChanges ();
            }
            /*

                        orderEmployee.Status = "Inprogress";
                        _context.Update(orderEmployee);
                        _context.SaveChanges();*/

            return RedirectToAction (nameof (Index));
        }

        #endregion

        #region StatusOpenAdmin

        public ActionResult OrderStatusOpenAdmin (string idMessageOpen) {
            var messageId = _context.OrderEmployeeAdmins.SingleOrDefault (m => m.Id == idMessageOpen);
            if (messageId != null && messageId.StatusAdmin == "Open") {

                messageId.StatusAdmin = "Open";

                _context.Update (messageId);
                _context.SaveChanges ();
            }

            return RedirectToAction (nameof (Index));
        }

        #endregion

    }
}