using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace inventory_accounting_system.Controllers {
    public class OrderEmployeesController : Controller {
        #region Dependency Injection
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderEmployeesController (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Index

        // GET: OrderEmployees
        //[Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Index (string status) {

            if (status == "New") {
                #region Search office Manager

                var userId = _userManager.GetUserId (User);

                var userFromOff = _context.Users.Where (u => u.IsDelete == false);
                foreach (var usr in userFromOff) {
                    if (await _userManager.IsInRoleAsync (usr, "Manager")) {
                        if (usr.Id == userId) {
                            #region Search office Admin

                            var userIdAdmin = _userManager.GetUserId (User);
                            var userNameAdmin = _userManager.GetUserName (User);

                            ViewData["UserIdAdmin"] = userNameAdmin;

                            var officeIdEmployeeAdmin = _context.Offices;

                            var userFromOffAdmin = _context.Users.Where (u => u.IsDelete == false);
                            foreach (var usrAdmin in userFromOffAdmin) {
                                if (await _userManager.IsInRoleAsync (usrAdmin, "Manager") && usrAdmin.Id == userIdAdmin) {
                                    ViewData["EmployeeToIdAdmin"] = new SelectList (_context.Users.Where (u => u.Id == usrAdmin.Id), "Id", "Name");
                                    var userOfficeIdAdmin = usrAdmin.OfficeId;

                                    foreach (var office in officeIdEmployeeAdmin) {
                                        if (userOfficeIdAdmin == office.Id) {
                                            foreach (var usrAdminOffice in userFromOffAdmin) {
                                                ViewData["OfficeIdAdmin"] = new SelectList (_context.Offices.Where (o => o.Id == usrAdmin.OfficeId), "Id", "Title");
                                                ViewData["OfficeIdTitleAdmin"] = office.Title;

                                                foreach (var admin in userFromOffAdmin) {
                                                    if (await _userManager.IsInRoleAsync (admin, "Admin")) {
                                                        ViewData["EmployeeFromIdAdmin"] = new SelectList (_context.Users.Where (u => u.Id == admin.Id), "Id", "Name");

                                                        ViewData["EmployeeFromIdNameAdmin"] = admin.Name;
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                            #endregion

                            var empOrdersU = _context.OrderEmployees
                                .Include (a => a.Asset)
                                .Include (a => a.Office)
                                .Include (a => a.EmployeeFrom)
                                .Include (a => a.EmployeeTo)
                                .Where (e => e.Status == "New")
                                .GroupBy (e => new { e.EmployeeFromId, e.EmployeeFrom.Name, e.Status })
                                .Select (g => new SotringEmployeeOrderViewModel {
                                    Id = g.Key.EmployeeFromId,
                                        SendFromName = g.Key.Name,
                                        StatusVM = g.Key.Status,
                                        OrderCount = g.Count ()
                                }).ToList ();

                            EmployeeOrderViewModel empOrderVMU = new EmployeeOrderViewModel () {
                                SotringEmployeeOrderViewModel = empOrdersU
                            };

                            return View (empOrderVMU);
                            // return View (await applicationDbContext.ToListAsync ());
                        }

                    }

                }
                #endregion

            } else if (status == "Open") {
                #region Search office Manager

                var userId = _userManager.GetUserId (User);

                var userFromOff = _context.Users.Where (u => u.IsDelete == false);
                foreach (var usr in userFromOff) {
                    if (await _userManager.IsInRoleAsync (usr, "Manager")) {
                        if (usr.Id == userId) {
                            #region Search office Admin

                            var userIdAdmin = _userManager.GetUserId (User);
                            var userNameAdmin = _userManager.GetUserName (User);

                            ViewData["UserIdAdmin"] = userNameAdmin;

                            var officeIdEmployeeAdmin = _context.Offices;

                            var userFromOffAdmin = _context.Users.Where (u => u.IsDelete == false);
                            foreach (var usrAdmin in userFromOffAdmin) {
                                if (await _userManager.IsInRoleAsync (usrAdmin, "Manager") && usrAdmin.Id == userIdAdmin) {
                                    ViewData["EmployeeToIdAdmin"] = new SelectList (_context.Users.Where (u => u.Id == usrAdmin.Id), "Id", "Name");
                                    var userOfficeIdAdmin = usrAdmin.OfficeId;

                                    foreach (var office in officeIdEmployeeAdmin) {
                                        if (userOfficeIdAdmin == office.Id) {
                                            foreach (var usrAdminOffice in userFromOffAdmin) {
                                                ViewData["OfficeIdAdmin"] = new SelectList (_context.Offices.Where (o => o.Id == usrAdmin.OfficeId), "Id", "Title");
                                                ViewData["OfficeIdTitleAdmin"] = office.Title;

                                                foreach (var admin in userFromOffAdmin) {
                                                    if (await _userManager.IsInRoleAsync (admin, "Admin")) {
                                                        ViewData["EmployeeFromIdAdmin"] = new SelectList (_context.Users.Where (u => u.Id == admin.Id), "Id", "Name");

                                                        ViewData["EmployeeFromIdNameAdmin"] = admin.Name;
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                            #endregion

                            var empOrdersU = _context.OrderEmployees
                                .Include (a => a.Asset)
                                .Include (a => a.Office)
                                .Include (a => a.EmployeeFrom)
                                .Include (a => a.EmployeeTo)
                                // .Where (e => e.Status == "New")
                                .Where (e => e.Status == "Open")
                                .GroupBy (e => new { e.EmployeeFromId, e.EmployeeFrom.Name, e.Status })
                                .Select (g => new SotringEmployeeOrderViewModel {
                                    Id = g.Key.EmployeeFromId,
                                        SendFromName = g.Key.Name,
                                        StatusVM = g.Key.Status,
                                        OrderCount = g.Count ()
                                }).ToList ();

                            EmployeeOrderViewModel empOrderVMU = new EmployeeOrderViewModel () {
                                SotringEmployeeOrderViewModel = empOrdersU
                            };

                            return View (empOrderVMU);
                            // return View (await applicationDbContext.ToListAsync ());
                        }

                    }

                }
                #endregion

            } else if (status == "All") {
                #region Search office Manager

                var userId = _userManager.GetUserId (User);

                var userFromOff = _context.Users.Where (u => u.IsDelete == false);
                foreach (var usr in userFromOff) {
                    if (await _userManager.IsInRoleAsync (usr, "Manager")) {
                        if (usr.Id == userId) {
                            #region Search office Admin

                            var userIdAdmin = _userManager.GetUserId (User);
                            var userNameAdmin = _userManager.GetUserName (User);

                            ViewData["UserIdAdmin"] = userNameAdmin;

                            var officeIdEmployeeAdmin = _context.Offices;

                            var userFromOffAdmin = _context.Users.Where (u => u.IsDelete == false);
                            foreach (var usrAdmin in userFromOffAdmin) {
                                if (await _userManager.IsInRoleAsync (usrAdmin, "Manager") && usrAdmin.Id == userIdAdmin) {
                                    ViewData["EmployeeToIdAdmin"] = new SelectList (_context.Users.Where (u => u.Id == usrAdmin.Id), "Id", "Name");
                                    var userOfficeIdAdmin = usrAdmin.OfficeId;

                                    foreach (var office in officeIdEmployeeAdmin) {
                                        if (userOfficeIdAdmin == office.Id) {
                                            foreach (var usrAdminOffice in userFromOffAdmin) {
                                                ViewData["OfficeIdAdmin"] = new SelectList (_context.Offices.Where (o => o.Id == usrAdmin.OfficeId), "Id", "Title");
                                                ViewData["OfficeIdTitleAdmin"] = office.Title;

                                                foreach (var admin in userFromOffAdmin) {
                                                    if (await _userManager.IsInRoleAsync (admin, "Admin")) {
                                                        ViewData["EmployeeFromIdAdmin"] = new SelectList (_context.Users.Where (u => u.Id == admin.Id), "Id", "Name");

                                                        ViewData["EmployeeFromIdNameAdmin"] = admin.Name;
                                                    }
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                            #endregion

                            var empOrdersU = _context.OrderEmployees
                                .Include (a => a.Asset)
                                .Include (a => a.Office)
                                .Include (a => a.EmployeeFrom)
                                .Include (a => a.EmployeeTo)
                                .GroupBy (e => new { e.EmployeeFromId, e.EmployeeFrom.Name, e.Status })
                                .Select (g => new SotringEmployeeOrderViewModel {
                                    Id = g.Key.EmployeeFromId,
                                        SendFromName = g.Key.Name,
                                        StatusVM = g.Key.Status,
                                        OrderCount = g.Count ()
                                }).ToList ();

                            EmployeeOrderViewModel empOrderVMU = new EmployeeOrderViewModel () {
                                SotringEmployeeOrderViewModel = empOrdersU
                            };

                            return View (empOrderVMU);
                            // return View (await applicationDbContext.ToListAsync ());
                        }

                    }

                }
                #endregion
            }

            var empOrders = _context.OrderEmployees
                .Include (a => a.Asset)
                .Include (a => a.Office)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo)
                .Where (e => e.Status == "New")
                .GroupBy (e => new { e.EmployeeFromId, e.EmployeeFrom.Name, e.Status })
                .Select (g => new SotringEmployeeOrderViewModel {
                    Id = g.Key.EmployeeFromId,
                        SendFromName = g.Key.Name,
                        StatusVM = g.Key.Status,
                        OrderCount = g.Count ()
                }).ToList ();

            EmployeeOrderViewModel empOrderVM = new EmployeeOrderViewModel () {
                SotringEmployeeOrderViewModel = empOrders
            };

            return View (empOrderVM);

        }

        public IActionResult ListOrderMsg (string id) {

            var userNameOrderFrom = _context.Users.Where (u => u.Id == id).FirstOrDefault ();
            ViewBag.UserNameOrderF = userNameOrderFrom.Name;

            var listMsgUsers = _context.OrderEmployees
                .Include (a => a.Asset)
                .Include (a => a.Office)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo)
                .Where (l => l.EmployeeFromId == id);

            return View (listMsgUsers);
        }

        public IActionResult ListOrderMsgOpen (string id) {

            var userNameOrderFrom = _context.Users.Where (u => u.Id == id).FirstOrDefault ();
            ViewBag.UserNameOrderF = userNameOrderFrom.Name;

            var listMsgUsers = _context.OrderEmployees
                .Include (a => a.Asset)
                .Include (a => a.Office)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo)
                .Where (l => l.EmployeeFromId == id);

            return View (listMsgUsers);
        }

        public IActionResult ListOrderMsgAll () {

            var listMsgUsers = _context.OrderEmployees
                .Include (a => a.Asset)
                .Include (a => a.Office)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo);

            return View (listMsgUsers);
        }

        #endregion

        #region OrderSend
        //[Authorize(Roles = "User, Admin")]
        public ActionResult OrderSend (string officeId, string title, string content, string employeeFromId, string employeeToId) {

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

        #region Status

        public IActionResult OrderStatus (string idMessage) {

            var messageId = _context.OrderEmployees.SingleOrDefault (m => m.Id == idMessage);
            if (messageId != null && messageId.Status == "New") {
                messageId.Status = "Open";
                messageId.Title = "Заявка на преобретение имущества";
                _context.Update (messageId);
                _context.SaveChanges ();

            }
            return RedirectToAction (nameof (Index));
        }

        public string GetMsgOrderStatus (string idMessage) {
            var msgStatusJson = String.Empty;
            var messageId = _context.OrderEmployees.SingleOrDefault (m => m.Id == idMessage);
            if (messageId != null) {
                string status = messageId.Status;
                string statusMsg = status;
                msgStatusJson = statusMsg;
            }
            return JsonConvert.SerializeObject (msgStatusJson);
        }

        #endregion

        #region StatusOpen

        public ActionResult OrderStatusOpen (string idMessageOpen) {
            var messageId = _context.OrderEmployees.SingleOrDefault (m => m.Id == idMessageOpen);
            if (messageId != null && messageId.Status == "Open") {

                messageId.Status = "Open";

                _context.Update (messageId);
                _context.SaveChanges ();
            }
            return RedirectToAction (nameof (Index));
        }

        #endregion

        #region OrderSendAdmin
        //[Authorize(Roles = "User, Admin")]
        public ActionResult OrderSendAdmin (
            string officeIdAdmin,
            string titleAdmin,
            string contentAdmin,
            string employeeFromIdAdmin,
            string employeeToIdAdmin,
            string idMessageOpen,
            OrderEmployee orderStatus) {
            OrderStatusInprogress (idMessageOpen);

            var orderSend = new OrderEmployeeAdmin {
                OfficeAdminId = officeIdAdmin,
                TitleAdmin = titleAdmin,
                ContentAdmin = contentAdmin,
                EmployeeFromAdminId = employeeFromIdAdmin,
                EmployeeToAdminId = employeeToIdAdmin,
                DateFromAdmin = DateTime.Now,
                DateToAdmin = null
            };

            var msg = _context.OrderEmployees;
            foreach (var orderEmployee in msg) {
                if (orderEmployee.Id == idMessageOpen) {
                    string contentUsr = orderEmployee.Content;
                    orderSend.ContentUser = contentUsr;
                }
            }
            _context.Add (orderSend);
            _context.SaveChanges ();

            return RedirectToAction (nameof (Index));
        }

        #endregion

        #region OrderStatusInprogress

        public ActionResult OrderStatusInprogress (string idMessageOpen) {
            var messageId = _context.OrderEmployees.SingleOrDefault (m => m.Id == idMessageOpen);
            if (messageId != null && messageId.Status == "Open") {

                messageId.Status = "Inprogress";
                messageId.DateTo = DateTime.Now;

                _context.Update (messageId);
                _context.SaveChanges ();
            }

            return RedirectToAction (nameof (Index));
        }

        #endregion

        #region Details

        // GET: OrderEmployees/Details/5
        public async Task<IActionResult> Details (string id) {
            if (id == null) {

                return NotFound ();
            }
            var userName = _userManager.GetUserName (Request.HttpContext.User);
            ViewData["UserId"] = userName;

            var orderEmployee = await _context.OrderEmployees
                .Include (o => o.EmployeeFrom)
                .Include (o => o.EmployeeTo)
                .Include (o => o.Office)
                .Include (o => o.Asset)
                .SingleOrDefaultAsync (m => m.Id == id);
            if (orderEmployee == null) {
                return NotFound ();
            }

            return View (orderEmployee);
        }

        #endregion

        #region Delete

        // GET: OrderEmployees/Delete/5
        public async Task<IActionResult> Delete (string id) {
            if (id == null) {
                return NotFound ();
            }

            var orderEmployee = await _context.OrderEmployees
                .Include (o => o.EmployeeFrom)
                .Include (o => o.Office)
                .SingleOrDefaultAsync (m => m.Id == id);
            if (orderEmployee == null) {
                return NotFound ();
            }

            return View (orderEmployee);
        }

        // POST: OrderEmployees/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (string id) {
            var orderEmployee = await _context.OrderEmployees.SingleOrDefaultAsync (m => m.Id == id);
            _context.OrderEmployees.Remove (orderEmployee);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool OrderEmployeeExists (string id) {
            return _context.OrderEmployees.Any (e => e.Id == id);
        }

        #endregion

        #region Check

        public ActionResult CheckViewComponent (string[] assetId, string officeId, string employeeId, string employeeFromId, string content) {

            foreach (var item in assetId) {
                var assetIdFind = _context.Assets.FirstOrDefault (a => a.Id == item);
                var assetIdFindOrder = _context.OrderEmployees.FirstOrDefault (a => a.AssetId == item || a.AssetId == null);

                if (assetIdFind != null && assetIdFindOrder == null) {

                    var orderSend = new OrderEmployee {
                    AssetId = assetIdFind.Id,
                    OfficeId = officeId,
                    Content = content,
                    EmployeeToId = employeeId,
                    EmployeeFromId = employeeFromId,
                    DateFrom = DateTime.Now,
                    Title = "Заявка на преобретение имущества",
                    DateTo = null
                    };
                    _context.Add (orderSend);
                    _context.SaveChanges ();

                } else {
                    ModelState.AddModelError ("AssetId", "Заявка на этот товар уже отправлена.");
                }
            }
            return Content ("Ок");
        }

        #endregion
    }
}