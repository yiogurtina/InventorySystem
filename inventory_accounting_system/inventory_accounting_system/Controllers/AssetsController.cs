using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using inventory_accounting_system.Services;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace inventory_accounting_system.Controllers {
    [Authorize]
    public class AssetsController : Controller {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        static Random generator = new Random ();
        private readonly IHostingEnvironment _appEnvironment;
        private readonly FileUploadService _fileUploadService;
        private readonly UserManager<Employee> _userManager;

        public AssetsController (ApplicationDbContext context, IHostingEnvironment appEnvironment,
            FileUploadService fileUploadService, UserManager<Employee> userManager) {
            _context = context;
            _appEnvironment = appEnvironment;
            _fileUploadService = fileUploadService;
            _userManager = userManager;
        }

        #endregion

        #region Index
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Index (string searchString, Sorting sorting = Sorting.NameAsc) {
            ViewData["OfficeId"] = new SelectList (_context.Offices, "Id", "Title");
            ViewData["EmployeeId"] = new SelectList (_context.Users, "Id", "Name");
            ViewData["dateAction"] = DateTime.Now.ToString ("yyyy-MM-dd");

            ViewData["OfficeIdSale"] = new SelectList (_context.Offices, "Id", "Title");
            var mainStorageOne = _context.Offices;
            foreach (var item in mainStorageOne) {
                if (item.Title == "Main storage") {

                    ViewData["OfficeIdSaleOne"] = item.Title;
                }
            }

            ViewData["EmployeeIdSale"] = new SelectList (_context.Users, "Id", "Name");
            ViewData["dateActionSale"] = DateTime.Now.ToString ("yyyy-MM-dd");

            IEnumerable<StatusMovingAssetsEnum> statusAssetsEnums = Enum.GetValues (typeof (StatusMovingAssetsEnum)).Cast<StatusMovingAssetsEnum> ().ToList ();
            ViewData["StatusMovingAssets"] = new SelectList (statusAssetsEnums.ToList ());

            foreach (var item in statusAssetsEnums) {
                if (item.ToString () == "sale") {

                    ViewData["StatusMovingAssetsOne"] = item.ToString ();
                }

            }

            var mainStorage = _context.Offices.FirstOrDefault (s => s.IsMain);
            var assets = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Supplier)
                .Include (a => a.Office)
                .Where (a => a.IsActive)
                .Where (a => a.InStock)
                .Where (a => a.OfficeId == mainStorage.Id);

            #region Sorting

            ViewData["IsActiveSort"] = sorting == Sorting.IsActiveAsc ? Sorting.IsActiveDesc : Sorting.IsActiveAsc;
            ViewData["NameSort"] = sorting == Sorting.NameAsc ? Sorting.NameDesc : Sorting.NameAsc;
            ViewData["InventNumberSort"] = sorting == Sorting.InventNumberAsc ? Sorting.InventNumberDesc : Sorting.InventNumberAsc;
            ViewData["DateSort"] = sorting == Sorting.DateAsc ? Sorting.DateDesc : Sorting.DateAsc;
            ViewData["ImageSort"] = sorting == Sorting.ImageAsc ? Sorting.ImageDesc : Sorting.ImageAsc;
            ViewData["DocumentSort"] = sorting == Sorting.DocumentAsc ? Sorting.DocumentDesc : Sorting.DocumentAsc;
            ViewData["CategorySort"] = sorting == Sorting.CategoryAsc ? Sorting.CategoryDesc : Sorting.CategoryAsc;
            ViewData["SupplierSort"] = sorting == Sorting.SupplierAsc ? Sorting.SupplierDesc : Sorting.SupplierAsc;
            ViewData["PriceSort"] = sorting == Sorting.PriceAsc ? Sorting.PriceDesc : Sorting.PriceAsc;

            switch (sorting) {
                case Sorting.IsActiveDesc:
                    assets = assets.OrderByDescending (s => s.IsActive.Equals (true));
                    break;
                case Sorting.NameDesc:
                    assets = assets.OrderByDescending (s => s.Name);
                    break;
                case Sorting.InventNumberDesc:
                    assets = assets.OrderByDescending (s => s.InventNumber.Length);
                    break;
                case Sorting.DateDesc:
                    assets = assets.OrderByDescending (s => s.Date.Minute);
                    break;
                case Sorting.ImageDesc:
                    assets = assets.OrderByDescending (s => s.Image.FileName);
                    break;
                case Sorting.DocumentDesc:
                    assets = assets.OrderByDescending (s => s.Document);
                    break;
                case Sorting.CategoryDesc:
                    assets = assets.OrderByDescending (s => s.Category.Name);
                    break;
                case Sorting.SupplierDesc:
                    assets = assets.OrderByDescending (s => s.Supplier.Name);
                    break;
                case Sorting.PriceDesc:
                    assets = assets.OrderByDescending(s => s.Price);
                    break;
                default:
                    assets = assets.OrderBy (s => s.Name);
                    break;
            }

            #endregion

            if (searchString != null) {
                var assets1 = from m in _context.Assets
                select m;

                if (!string.IsNullOrEmpty (searchString)) {
                    assets1 = assets1.Where (s => s.Name.Contains (searchString));
                }
                return View (await assets1.ToListAsync ());
            }

            return View (await assets.ToListAsync ());

        }

        #endregion

        #region CategoryAssets

        public async Task<IActionResult> CategoryAssets (string officeId, string categoryId) {
            ViewData["OfficeId"] = new SelectList (_context.Offices, "Id", "Title");
            ViewData["EmployeeId"] = new SelectList (_context.Users, "Id", "Name");
            ViewData["dateAction"] = DateTime.Now.ToString ("yyyy-MM-dd");

            var assets = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Supplier)
                .Where (a => a.IsActive == true)
                .Where (a => a.InStock == false)
                .Where (a => a.OfficeId == officeId)
                .Where (a => a.CategoryId == categoryId);
            return View (await assets.ToListAsync ());
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details (string id) {
            if (id == null) {
                return NotFound ();
            }

            var asset = await _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Supplier)
                .SingleOrDefaultAsync (m => m.Id == id);
            if (asset == null) {
                return NotFound ();
            }

            DetailsAssetViewModel model = new DetailsAssetViewModel () {
                Asset = asset,
                AssetsMoveStories = _context.AssetsMoveStories
                .Where (f => f.AssetId == id)
                .Include (t => t.EmployeeFrom)
                .Include (t => t.OfficeFrom)
                .Include (t => t.EmployeeTo)
                .Include (t => t.OfficeTo)
                .OrderBy (t => t.DateCurrent)
            };

            return View (model);
            //return View(asset);
        }

        #endregion

        public string GetCategoryEvents (string categoryId) {
            if (categoryId == null) throw new Exception ();
            var events = _context.Events.Where (e => e.CategoryId == categoryId);

            return JsonConvert.SerializeObject (events);
        }

        #region Create

        public IActionResult Create () {
            string usrId = _userManager.GetUserId (User);
            var user = _context.Users.Find (usrId);

            ViewData["CategoryId"] = new SelectList (_context.Categories, "Id", "Name");
            ViewData["SupplierId"] = new SelectList (_context.Suppliers, "Id", "Name");
            if (User.IsInRole ("Admin")) {
                ViewData["EmployeeId"] = new SelectList (_userManager.Users, "Id", "Name");
            } else {
                ViewData["EmployeeId"] = new SelectList (_userManager.Users.Where (u => u.OfficeId == user.OfficeId), "Id", "Name");
            }
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Name,CategoryId,InventNumber,InventPrefix,Date,OfficeId,StorageId,SupplierId,EmployeeId,Id,Image, Document, StatusMovingAssets, Price")] Asset asset,
            string serialNum,
            string eventId) {

            var storage = _context.Offices.FirstOrDefault (s => s.IsMain);
            var categoryPrefix = _context.Categories
                .Where (c => c.Id == asset.CategoryId)
                .Select (c => c.Prefix)
                .FirstOrDefaultAsync ();
            var admin = await _userManager.FindByNameAsync ("admin");

            asset.InventNumber = categoryPrefix.Result + generator.Next (0, 1000000).ToString ("D7");

            if (ModelState.IsValid) {

                asset.SerialNum = serialNum;

                InventoryNumberHistory inventoryNumberHistory = new InventoryNumberHistory {
                    Been = asset.InventNumber,
                    CreateDate = DateTime.Now,
                    AssetIdCreate = asset.Id

                };
                _context.Add (inventoryNumberHistory);

                asset.IsActive = true;
                asset.InStock = true;
                if (storage != null) {
                    asset.OfficeId = storage.Id;
                    asset.EmployeeId = admin.Id;
                }

                if (asset.Image != null) {
                    UploadPhoto (asset);
                } else {
                    asset.ImagePath = "images/default-image.jpg";
                }
                if (asset.Document != null) {
                    UploadDocument (asset);
                }

                if (eventId != null) {
                    int period;
                    var _event = _context.Events.Find (eventId);
                    switch (_event.Periodicity) {
                        case "Ежедневно":
                            period = 1;
                            break;
                        case "Еженедельно":
                            period = 7;
                            break;
                        case "Ежемесячно":
                            period = 31;
                            break;
                        case "Ежегодно":
                            period = 365;
                            break;
                        default:
                            period = 0;
                            break;
                    }
                    var assetEvent = new EventAsset () {
                        Title = _event.Title,
                        Content = _event.Content,
                        Period = period,
                        CreationDate = DateTime.Now,
                        DeadLine = DateTime.Now.AddDays (period),
                        AssetId = asset.Id,
                        EmployeeId = asset.EmployeeId
                    };
                    _context.Add (assetEvent);
                }

                _context.Add (asset);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            ViewData["CategoryId"] = new SelectList (_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList (_context.Suppliers, "Id", "Name", asset.SupplierId);
            return View (asset);
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit (string id) {
            if (id == null) {
                return NotFound ();
            }

            var asset = await _context.Assets.SingleOrDefaultAsync (m => m.Id == id);

            if (asset == null) {
                return NotFound ();
            }
            ViewData["CategoryId"] = new SelectList (_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList (_context.Suppliers, "Id", "Name", asset.SupplierId);
            return View (asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (string id, [Bind ("Name,CategoryId,InventNumber,Date,OfficeId,StorageId,SupplierId,EmployeeId,Image,Id, Price")] Asset asset,
            InventoryNumberHistory inventNumberHistory,
            string serialNum,
            string currentPath,
            string inventNumber) {
            if (id != asset.Id) {
                return NotFound ();
            }

            var inventNumSearch = _context.Assets
                .FirstOrDefault (i => i.InventNumber == inventNumber);

            if (inventNumSearch != null) {
                ModelState.AddModelError ("InventNumber", "Такой номер уже существует");
            }

            var oldInventoryNumber = _context.InventoryNumberHistories.FirstOrDefault (i => i.AssetIdCreate == asset.Id);

            if (ModelState.IsValid) {
                try {
                    if (oldInventoryNumber != null) {
                        oldInventoryNumber.Become = inventNumber;
                        oldInventoryNumber.ChangeDate = DateTime.Now;
                        _context.Update (oldInventoryNumber);
                    }

                    asset.SerialNum = serialNum;
                    if (asset.Image != null) {
                        UploadPhoto (asset);
                    } else {
                        asset.ImagePath = currentPath;
                    }

                    asset.InventNumber = inventNumber;
                    asset.IsActive = true;
                    _context.Update (asset);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!AssetExists (asset.Id)) {
                        return NotFound ();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }

            ViewData["CategoryId"] = new SelectList (_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList (_context.Suppliers, "Id", "Name", asset.SupplierId);
            return View (asset);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete (string id) {
            if (id == null) {
                return NotFound ();
            }

            var asset = await _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Supplier)
                .SingleOrDefaultAsync (m => m.Id == id);
            if (asset == null) {
                return NotFound ();
            }

            return View (asset);
        }

        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (string id) {
            var asset = await _context.Assets.SingleOrDefaultAsync (m => m.Id == id);
            asset.IsActive = false;
            _context.Update (asset);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        #endregion

        #region Move

        public async Task<IActionResult> Move (string id) {
            if (id == null) {
                return NotFound ();
            }

            var asset = await _context.Assets.SingleOrDefaultAsync (m => m.Id == id);
            if (asset == null) {
                return NotFound ();
            }
            ViewData["CategoryId"] = new SelectList (_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList (_context.Suppliers, "Id", "Name", asset.SupplierId);
            ViewData["AssetId"] = id;

            var assetsMoveStory = new AssetsMoveStory ();
            ViewData["EmployeeFromId"] = new SelectList (_context.Users, "Id", "Login");
            ViewData["OfficeFromId"] = new SelectList (_context.Offices, "Id", "Title");

            ViewData["EmployeeToId"] = new SelectList (_context.Users, "Id", "Login");
            ViewData["OfficeToId"] = new SelectList (_context.Offices, "Id", "Title");

            return View ("Move", new MoveViewModel () {
                Asset = asset,
                    AssetsMoveStory = assetsMoveStory,
                    AssetsMoveStories = _context.AssetsMoveStories
                    .Where (f => f.AssetId == id)
                    .Include (t => t.EmployeeFrom)
                    .Include (t => t.OfficeFrom)
                    .Include (t => t.EmployeeTo)
                    .Include (t => t.OfficeTo)
            });
            //return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Move (AssetsMoveStory assetsMoveStory) {

            if (ModelState.IsValid) {
                try {
                    _context.Add (assetsMoveStory);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!AssetExists (assetsMoveStory.Id)) {
                        return NotFound ();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            ViewData["EmployeeFromId"] = new SelectList (_context.Users, "Id", "Login", assetsMoveStory.EmployeeFromId);
            ViewData["OfficeFromId"] = new SelectList (_context.Offices, "Id", "Title", assetsMoveStory.OfficeFromId);

            ViewData["EmployeeToId"] = new SelectList (_context.Users, "Id", "Login", assetsMoveStory.EmployeeToId);
            ViewData["OfficeToId"] = new SelectList (_context.Offices, "Id", "Title", assetsMoveStory.OfficeToId);
            return View (assetsMoveStory);
        }
        #endregion

        #region AssetExists

        private bool AssetExists (string id) {
            return _context.Assets.Any (e => e.Id == id);
        }

        #endregion

        #region UploadPhoto

        private void UploadPhoto (Asset asset) {
            var path = Path.Combine (_appEnvironment.WebRootPath, $"images\\{asset.Name}\\image");
            _fileUploadService.Upload (path, asset.Image.FileName, asset.Image);
            asset.ImagePath = $"images/{asset.Name}/image/{asset.Image.FileName}";
        }
        #endregion

        #region UploadDocument

        private void UploadDocument (Asset asset) {
            var path = Path.Combine (_appEnvironment.WebRootPath, $"documents\\{asset.Name}\\document");
            _fileUploadService.Upload (path, asset.Document.FileName, asset.Document);
            ///*asset.DocumentPath = $"documents/{asset.Name}/document/{asset.Document.FileName*/}"
        }

        #endregion

        #region Download

        public ActionResult Download (string id) {
            var assetA = _context.Assets.FirstOrDefault (x => x.Id == id);

            byte[] b;
            if (assetA.DocumentPath != null) {
                b = System.IO.File.ReadAllBytes (assetA.DocumentPath);
                string fileName = "myfile.ext";
                return File (b, MediaTypeNames.Application.Octet, fileName);
            } else {
                return View ("Error");;
            }
        }

        #endregion

        #region Check

        public ActionResult Check (string[] assetId, string officeId, string employeeId, string dateAction, int inIndex) {
            foreach (var item in assetId) {
                var assetIdFind = _context.Assets.FirstOrDefault (a => a.Id == item);
                if (assetIdFind != null) {
                    string officeFromId = assetIdFind.OfficeId;
                    string employeeFromId = assetIdFind.EmployeeId;
                    string officeToId = officeId;
                    string employeeToId = employeeId;

                    var OfficeFind = _context.Offices.FirstOrDefault (a => a.Id == officeId);

                    assetIdFind.InStock = false;
                    if (OfficeFind.IsMain) {
                        assetIdFind.InStock = true;
                    }

                    assetIdFind.OfficeId = officeId;
                    assetIdFind.EmployeeId = employeeId;
                    _context.Update (assetIdFind);
                    _context.SaveChanges ();

                    //DateTime dateStart = DateTime.Parse(dateAction);
                    DateTime dateStart = DateTime.Parse ("2019-01-18 0:00");
                    DateTime dateEnd = DateTime.Parse ("2100-01-01 0:00");

                    AssetsMoveStory assetsMoveStory = new AssetsMoveStory {
                        AssetId = assetIdFind.Id,
                        EmployeeFromId = employeeFromId,
                        OfficeFromId = officeFromId,
                        EmployeeToId = employeeToId,
                        OfficeToId = officeToId,
                        DateStart = dateStart,
                        DateEnd = dateEnd
                    };

                    _context.Add (assetsMoveStory);
                    _context.SaveChanges ();

                }
            }
            if (inIndex == 1) {
                return RedirectToAction (nameof (Index));
            } else {
                // return RedirectToAction("CategoryAssets", "Assets", new { officeId = officeId, categoryId =employeeId });
                return RedirectToAction ("Index", "Offices");
            }

            //
        }

        #endregion

        #region SaleAsste

        public ActionResult SaleAsste (
            string[] assetIdSale,
            string officeIdSale,
            string employeeIdSale,
            int inIndex,
            string statusAssetsMving) {

            foreach (var item in assetIdSale) {
                var assetIdFind = _context.Assets.FirstOrDefault (a => a.Id == item);
                if (assetIdFind != null) {
                    assetIdFind.IsActive = false;
                    assetIdFind.StatusMovingAssets = statusAssetsMving;
                    assetIdFind.InStock = false;
                    assetIdFind.OfficeId = officeIdSale;
                    assetIdFind.Date = DateTime.Now;
                    _context.Update (assetIdFind);
                    _context.SaveChanges ();

                    //DateTime dateStart = DateTime.Parse(dateAction);
                    DateTime dateStart = DateTime.Parse ("2019-01-18 0:00");
                    DateTime dateEnd = DateTime.Parse ("2100-01-01");

                    foreach (var itemMove in assetIdSale) {

                        var actionMove = _context.AssetsMoveStories.FirstOrDefault (s => s.Id == itemMove);
                        if (actionMove == null) {

                            AssetsMoveStory assetsMoveStory = new AssetsMoveStory {
                            AssetId = assetIdFind.Id,
                            EmployeeFromId = assetIdFind.EmployeeId,
                            OfficeFromId = assetIdFind.OfficeId,
                            EmployeeToId = employeeIdSale,
                            OfficeToId = officeIdSale,
                            StatusMovinHistory = statusAssetsMving,
                            DateStart = dateStart,
                            DateEnd = dateEnd
                            };

                            _context.Add (assetsMoveStory);
                            _context.SaveChanges ();
                        }
                    }

                }
            }
            if (inIndex == 1) {
                return RedirectToAction (nameof (Index));
            } else {
                // return RedirectToAction("CategoryAssets", "Assets", new { officeId = officeId, categoryId =employeeId });
                return RedirectToAction ("Index", "Offices");
            }

            //
        }

        #endregion

    }
}