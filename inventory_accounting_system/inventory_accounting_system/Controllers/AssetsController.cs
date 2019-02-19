using System;
using System.Collections.Generic;
using System.Globalization;
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
using ZXing;
using ZXing.Common;
using ZXing.CoreCompat.System.Drawing;

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
        public async Task<IActionResult> Index (string searchString, Sorting sorting = Sorting.NameAsc, int page = 1) {

            int pageSize = 7;

            //TODO: Переделать пагинацию с помощью таг-хелперов, так же исправить баг, сохраннение выбранного чекбокса при переходе на следующею страницу пагинации

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
            IQueryable<Asset> assets = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Supplier)
                .Include (a => a.Office)
                .Where (a => a.IsActive)
                .Where (a => a.InStock)
                .Where (a => a.OfficeId == mainStorage.Id);

            var count = await assets.CountAsync ();
            var items = await assets.Skip ((page - 1) * pageSize).Take (pageSize).ToListAsync ();

            PageVM pageVM = new PageVM (count, page, pageSize);
            IndexVM viewModel = new IndexVM {
                PageVM = pageVM,
                Assets = items
            };

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
                    assets = assets.OrderByDescending (s => s.Price);
                    break;
                default:
                    assets = assets.OrderBy (s => s.Name);
                    break;
            }

            #endregion

            if (searchString != null) {
                IQueryable<Asset> assets1 = from m in _context.Assets
                select m;

                if (!string.IsNullOrEmpty (searchString)) {
                    assets1 = assets1.Where (s => s.Name.Contains (searchString));

                }
                PageVM pageVM1 = new PageVM (count, page, pageSize);
                IndexVM viewModel1 = new IndexVM {
                    PageVM = pageVM1,
                    Assets = assets1
                };
                return View (viewModel1);
            }

            return View (viewModel);

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
                Barcode = GetBarcode (asset.InventNumber),
                AssetsMoveStories = _context.AssetsMoveStories
                .Where (f => f.AssetId == id)
                .Include (t => t.EmployeeFrom)
                .Include (t => t.OfficeFrom)
                .Include (t => t.EmployeeTo)
                .Include (t => t.OfficeTo)
                .OrderBy (t => t.DateCurrent),
                Documents = _context.Documents
                .Where (f => f.AssetId == id)
            };

            return View (model);
        }

        public IActionResult GetFile (string documentId) {
            var doc = _context.Documents.FirstOrDefault (d => d.Id == documentId);

            string filePath = Path.Combine ("~", doc.Path);

            string fileName = doc.Name;

            //return PhysicalFile(filePath, fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes (filePath);

            return File (fileBytes, "application/force-download", fileName);
        }

        public string GetCategoryEvents (string categoryId) {
            if (categoryId == null) throw new Exception ();
            var events = _context.Events.Where (e => e.CategoryId == categoryId);

            return JsonConvert.SerializeObject (events);
        }

        #endregion

        #region GetBarcode

        public string GetBarcode (string invent) {
            BarcodeWriter writer = new BarcodeWriter {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions {
                Height = 70,
                Width = 170,
                PureBarcode = false,
                Margin = 10
                }
            };

            var barCodeImage = writer.Write (invent);
            byte[] byteImage;
            using (var stream = new MemoryStream ()) {
                barCodeImage.Save (stream, System.Drawing.Imaging.ImageFormat.Png);
                byteImage = stream.ToArray ();
                return Convert.ToBase64String (byteImage);
            }
        }

        #endregion

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
            string eventId,
            int? count) {

            count = count == null || count <= 0 ? 1 : count;

            var storage = _context.Offices.FirstOrDefault (s => s.IsMain);
            var categoryPrefix = _context.Categories
                .Where (c => c.Id == asset.CategoryId)
                .Select (c => c.Prefix)
                .FirstOrDefaultAsync ();
            var admin = await _userManager.FindByNameAsync ("admin");

            if (ModelState.IsValid) {

                asset.SerialNum = serialNum;

                InventoryNumberHistory inventoryNumberHistory = new InventoryNumberHistory {
                    Been = asset.InventNumber,
                    Become = asset.InventNumber,
                    CreateDate = DateTime.Now,
                    AssetIdCreate = asset.Id

                };

                AddAssetsCount (inventoryNumberHistory, count, categoryPrefix);

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

                AddAssetsCount (asset, count, categoryPrefix);

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
                    AddAssetsCount (assetEvent, count);
                }
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

            EditAssetViewModel model = new EditAssetViewModel () {
                Id = asset.Id,
                Name = asset.Name,
                CategoryId = asset.CategoryId,
                OfficeId = asset.OfficeId,
                EmployeeId = asset.EmployeeId,
                InventNumber = asset.InventNumber,
                Date = asset.Date,
                SupplierId = asset.SupplierId,
                SerialNum = asset.SerialNum,
                ImagePath = asset.ImagePath,
                Price = asset.Price,
                InStock = asset.InStock,
                IsActive = asset.IsActive
            };

            return View (model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (EditAssetViewModel editAsset) {
            if (ModelState.IsValid) {
                try {
                    var asset = _context.Assets.FirstOrDefault (i => i.Id == editAsset.Id);

                    if (editAsset.OldInventNumber != editAsset.InventNumber) {
                        InventoryNumberHistory inventoryNumberHistory = new InventoryNumberHistory {
                        Been = editAsset.OldInventNumber,
                        Become = editAsset.InventNumber,
                        CreateDate = DateTime.Now,
                        AssetIdCreate = asset.Id
                        };
                        _context.Add (inventoryNumberHistory);
                    }

                    if (asset.Image != null) {
                        UploadPhoto (asset);
                    } else {
                        asset.ImagePath = editAsset.ImagePath;
                    }

                    asset.Name = editAsset.Name;
                    asset.CategoryId = editAsset.CategoryId;
                    asset.SupplierId = editAsset.SupplierId;
                    asset.EmployeeId = editAsset.EmployeeId;
                    asset.OfficeId = editAsset.OfficeId;
                    asset.InventNumber = editAsset.InventNumber;
                    asset.SerialNum = asset.SerialNum;
                    asset.Price = asset.Price;
                    //asset.InStock = true;
                    asset.IsActive = true;
                    _context.Update (asset);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!AssetExists (editAsset.Id)) {
                        return NotFound ();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction (nameof (Index));
            }

            ViewData["CategoryId"] = new SelectList (_context.Categories, "Id", "Name", editAsset.CategoryId);
            ViewData["SupplierId"] = new SelectList (_context.Suppliers, "Id", "Name", editAsset.SupplierId);

            EditAssetViewModel model = new EditAssetViewModel () {
                Id = editAsset.Id,
                Name = editAsset.Name,
                CategoryId = editAsset.CategoryId,
                OfficeId = editAsset.OfficeId,
                EmployeeId = editAsset.EmployeeId,
                InventNumber = editAsset.InventNumber,
                Date = editAsset.Date,
                SupplierId = editAsset.SupplierId,
                SerialNum = editAsset.SerialNum,
                ImagePath = editAsset.ImagePath,
                Price = editAsset.Price,
                InStock = editAsset.InStock,
                IsActive = editAsset.IsActive
            };

            return View (model);
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

        #region InventNumberSearch
        public ActionResult InventNumberSearch (string inventNumber) {
            var asset = _context.Assets.FirstOrDefault (s => s.InventNumber == inventNumber);

            if (asset != null) {
                return RedirectToAction ("Details", "Assets", new { id = asset.Id });
            } else {
                return RedirectToAction ("Index", "Offices");
            }

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

        [HttpPost]
        public async Task<IActionResult> UploadDocument (IFormFile uploadedFile, string assetId) {
            if (uploadedFile != null) {

                var uniqueFileName = GetUniqueFileName (uploadedFile.FileName);
                var uploads = "/Documents/";
                var path = Path.Combine (uploads, uniqueFileName);

                using (var fileStream = new FileStream (_appEnvironment.WebRootPath + path, FileMode.Create)) {
                    await uploadedFile.CopyToAsync (fileStream);
                }

                Document file = new Document { AssetId = assetId, Name = uploadedFile.FileName, Path = path };
                _context.Documents.Add (file);
                _context.SaveChanges ();
            }

            return RedirectToAction ("Details", "Assets", new { id = assetId });
        }

        private string GetUniqueFileName (string fileName) {
            fileName = Path.GetFileName (fileName);
            return Path.GetFileNameWithoutExtension (fileName) +
                "_" +
                Guid.NewGuid ().ToString ().Substring (0, 4) +
                Path.GetExtension (fileName);
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

        #region AssetsMove

        public ActionResult AssetsMove (
            string[] assetId,
            string officeId,
            string employeeId,
            string dateAction,
            int inIndex) {

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

                    DateTime dateStart = DateTime.Parse (dateAction);
                    //DateTime dateStart = DateTime.Parse ("2019-01-18 0:00");
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

                    if (assetIdFind.InStock == false) {
                        assetsMoveStory.StatusMovinHistory = "transfer_in";
                    } else {
                        assetsMoveStory.StatusMovinHistory = "transfer_out";
                    }

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

        #region Report 

        public IActionResult ReportOnStockChoice () {

            ViewData["CategoryList"] = new SelectList (_context.Categories, "Id", "Name");
            ViewData["OfficeList"] = new SelectList (_context.Offices, "Id", "Title");

            return View ();
        }

        [HttpPost]
        public IActionResult ReportOnStockResult (string datefrom, string dateto) {

            IQueryable<AssetsMoveStory> assetsMs = _context.AssetsMoveStories
                .Include (a => a.OfficeFrom)
                .Include (a => a.OfficeTo)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo)
                .Include (a => a.Asset);

            if (datefrom == null || dateto == null) {
                return RedirectToAction (nameof (ReportOnStockChoice));
            }
            DateTime dtFrom = DateTime.ParseExact (datefrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DateTime dtTo = DateTime.ParseExact (dateto, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            assetsMs = assetsMs.Where (a => a.DateStart >= dtFrom && a.DateStart <= dtTo);

            return View (assetsMs.ToList ());
        }

        [HttpPost]
        public IActionResult ReportOnStockNew (string datefrom) {

            IQueryable<Asset> assetsNew = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Office)
                .Include (a => a.Employee)
                .Include (a => a.Supplier);

            if (datefrom == null) {
                return RedirectToAction (nameof (ReportOnStockChoice));
            }
            DateTime dtFrom = DateTime.ParseExact (datefrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            assetsNew = assetsNew.Where (a => a.Date.Year == dtFrom.Year && a.Date.Day == dtFrom.Day && a.Date.Month == dtFrom.Month);

            return View (assetsNew);
        }

        [HttpPost]
        public IActionResult ReportOnStockAdd (string datefrom, string dateto) {

            IQueryable<Asset> assetsNew = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Office)
                .Include (a => a.Employee)
                .Include (a => a.Supplier);

            if (datefrom == null || dateto == null) {
                return RedirectToAction (nameof (ReportOnStockChoice));
            }

            DateTime dtFrom = DateTime.ParseExact (datefrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DateTime dtTo = DateTime.ParseExact (dateto, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            assetsNew = assetsNew.Where (a => a.Date >= dtFrom && a.Date <= dtTo);

            return View (assetsNew.ToList ());
        }

        [HttpPost]
        public IActionResult ReportOnStockShortFile (string datefrom, string dateto) {

            IQueryable<Asset> assetsNew = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Office)
                .Include (a => a.Employee)
                .Include (a => a.Supplier);

            if (datefrom == null || dateto == null) {
                return RedirectToAction (nameof (ReportOnStockChoice));
            }

            DateTime dtFrom = DateTime.ParseExact (datefrom, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            DateTime dtTo = DateTime.ParseExact (dateto, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            assetsNew = assetsNew.Where (a => a.StatusMovingAssets == "short_file");

            return View (assetsNew.ToList ());
        }

        #endregion

        #region AssetEvents

        public IActionResult EditAssetEvents (string assetId) {
            ViewData["AssetId"] = assetId;
            var asset = _context.Assets.Include (a => a.AssetEvents).FirstOrDefault (a => a.Id == assetId);
            var _events = asset.AssetEvents;
            return View (_events);
        }

        #endregion

        #region AddAssets

        private void AddAssetsCount (InventoryNumberHistory inventoryNumberHistory, int? count, Task<string> categoryPrefix) {
            if (count == 1) {
                _context.Add (inventoryNumberHistory);
            } else {
                for (int i = 0; i < count; i++) {
                    inventoryNumberHistory.Been = categoryPrefix.Result + generator.Next (0, 1000000).ToString ("D7");
                    inventoryNumberHistory.Become = categoryPrefix.Result + generator.Next (0, 1000000).ToString ("D7");
                    inventoryNumberHistory.Id = Guid.NewGuid ().ToString ();
                    _context.InventoryNumberHistories.Add (inventoryNumberHistory);
                    _context.SaveChanges ();
                }
            }
        }

        private void AddAssetsCount (EventAsset eventAsset, int? count) {
            if (count == 1) {
                _context.Add (eventAsset);
            } else {
                for (int i = 0; i < count; i++) {
                    eventAsset.Id = Guid.NewGuid ().ToString ();
                    _context.AssetEvents.Add (eventAsset);
                    try {
                        _context.SaveChanges ();
                    } catch (Exception ex) {
                        Console.WriteLine (ex.Message);
                    }
                }
            }
        }
        private void AddAssetsCount (Asset asset, int? count, Task<string> categoryPrefix) {
            if (count == 1) {
                asset.InventNumber = categoryPrefix.Result + generator.Next (0, 1000000).ToString ("D7");
                _context.Add (asset);
            } else {
                for (int i = 0; i < count; i++) {
                    asset.Id = Guid.NewGuid ().ToString ();
                    asset.InventNumber = categoryPrefix.Result + generator.Next (0, 1000000).ToString ("D7");
                    _context.Assets.Add (asset);
                    _context.SaveChanges ();
                }
            }
        }

        #endregion

        #region EmployeeOrderReport 

        public IActionResult EmployeeOrderReport (string employeeId) {
            IQueryable<Asset> empId = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Office)
                .Include (a => a.Employee)
                .Include (a => a.Supplier);

            if (employeeId == null) {
                return RedirectToAction (nameof (ReportOnStockChoice));
            }

            empId = empId.Where (a => a.EmployeeId == employeeId);

            var resultEmp = _context.Users.FirstOrDefault (u => u.Id == employeeId);

            ViewData["EmployeeName"] = resultEmp.Name.ToString ();

            return View (empId);
        }

        #endregion

        #region CategoryList 

        [HttpPost]
        public IActionResult CategoryList (string categoryId) {

            IQueryable<Asset> categoryPrint = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Office)
                .Include (a => a.Employee)
                .Include (a => a.Supplier);

            if (categoryId == null) {
                return RedirectToAction (nameof (ReportOnStockChoice));
            }

            categoryPrint = categoryPrint.Where (c => c.CategoryId == categoryId);

            var resultEmp = _context.Categories.FirstOrDefault (u => u.Id == categoryId);

            ViewData["CategoryName"] = resultEmp.Name.ToString ();

            return View (categoryPrint);
        }

        #endregion

        #region ListOfAssetsByOffice 

        [HttpPost]
        public IActionResult ListOfAssetsByOffice (string officeId, string date) {

            IQueryable<AssetsMoveStory> officePrint = _context.AssetsMoveStories
                .Include (a => a.OfficeFrom)
                .Include (a => a.OfficeTo)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo)
                .Include (a => a.Asset);

            if (officeId == null || date == null) {
                return RedirectToAction (nameof (ReportOnStockChoice));
            }

            DateTime dt = DateTime.ParseExact (date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            officePrint = officePrint.Where (c => c.OfficeToId == officeId && c.DateStart == dt);

            var resultEmp = _context.Offices.FirstOrDefault (u => u.Id == officeId);

            ViewData["OfficeTitle"] = resultEmp.Title.ToString ();

            return View (officePrint);
        }

        #endregion
    }
}