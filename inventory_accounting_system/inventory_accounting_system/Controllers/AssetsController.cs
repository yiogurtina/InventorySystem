using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using System.IO;
using System.Net.Mime;
using Microsoft.AspNetCore.Hosting;
using inventory_accounting_system.Services;
using Microsoft.AspNetCore.Http;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace inventory_accounting_system.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        static Random generator = new Random();
        private readonly IHostingEnvironment _appEnvironment;
        private readonly FileUploadService _fileUploadService;
        private readonly UserManager<Employee> _userManager;

        public AssetsController(ApplicationDbContext context, IHostingEnvironment appEnvironment,
            FileUploadService fileUploadService, UserManager<Employee> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _fileUploadService = fileUploadService;
            _userManager = userManager;
        }

        #endregion

        #region Index
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(Sorting sorting = Sorting.NameAsc)
        {
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Title");

            var mainStorage = _context.Storages.FirstOrDefault(s=>s.IsMain);
            var assets = _context.Assets
                .Include(a => a.Category)
                .Include(a=>a.Storage)
                .Include(a => a.Supplier)
                .Where(a => a.IsActive)
                .Where(a => a.StorageId == mainStorage.Id);

            #region Sorting

            ViewData["IsActiveSort"] = sorting == Sorting.IsActiveAsc ? Sorting.IsActiveDesc : Sorting.IsActiveAsc;
            ViewData["NameSort"] = sorting == Sorting.NameAsc ? Sorting.NameDesc : Sorting.NameAsc;
            ViewData["InventNumberSort"] = sorting == Sorting.InventNumberAsc ? Sorting.InventNumberDesc : Sorting.InventNumberAsc;
            ViewData["DateSort"] = sorting == Sorting.DateAsc ? Sorting.DateDesc : Sorting.DateAsc;
            ViewData["ImageSort"] = sorting == Sorting.ImageAsc ? Sorting.ImageDesc : Sorting.ImageAsc;
            ViewData["DocumentSort"] = sorting == Sorting.DocumentAsc ? Sorting.DocumentDesc : Sorting.DocumentAsc;
            ViewData["CategorySort"] = sorting == Sorting.CategoryAsc ? Sorting.CategoryDesc : Sorting.CategoryAsc;
            ViewData["SupplierSort"] = sorting == Sorting.SupplierAsc ? Sorting.SupplierDesc : Sorting.SupplierAsc;

            switch (sorting)
            {
                case Sorting.IsActiveDesc:
                    assets = assets.OrderByDescending(s => s.IsActive.Equals(true));
                    break;
                case Sorting.NameDesc:
                    assets = assets.OrderByDescending(s => s.Name);
                    break;
                case Sorting.InventNumberDesc:
                    assets = assets.OrderByDescending(s => s.InventNumber.Length);
                    break;
                case Sorting.DateDesc:
                    assets = assets.OrderByDescending(s => s.Date.Minute);
                    break;
                case Sorting.ImageDesc:
                    assets = assets.OrderByDescending(s => s.Image.FileName);
                    break;
                case Sorting.DocumentDesc:
                    assets = assets.OrderByDescending(s => s.Document);
                    break;
                case Sorting.CategoryDesc:
                    assets = assets.OrderByDescending(s => s.Category.Name);
                    break;
                case Sorting.SupplierDesc:
                    assets = assets.OrderByDescending(s => s.Supplier.Name);
                    break;
                default:
                    assets = assets.OrderBy(s => s.Name);
                    break;
            }

            #endregion

            return View(await assets.ToListAsync());
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        #endregion

        public string GetCategoryEvents(string categoryId)
        {
            if (categoryId == null) throw new Exception();
            var events = _context.Events.Where(e => e.CategoryId == categoryId);
            
            return JsonConvert.SerializeObject(events);
        }

        #region Create

        public IActionResult Create()
        {
            string usrId =_userManager.GetUserId(User);
            var user = _context.Users.Find(usrId);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            if (User.IsInRole("Admin"))
            {
                ViewData["EmployeeId"] = new SelectList(_userManager.Users, "Id", "Name");
            }
            else
            {
                ViewData["EmployeeId"] = new SelectList(_userManager.Users.Where(u => u.OfficeId == user.OfficeId), "Id", "Name");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryId,InventNumber,InventPrefix,Date,OfficeId,StorageId,SupplierId,EmployeeId,Id,Image, Document")] Asset asset, string serialNum, string eventId)
        {
            var storage = _context.Storages.FirstOrDefault(s => s.IsMain);
            var categoryPrefix = _context.Categories
                .Where(c => c.Id == asset.CategoryId)
                .Select(c => c.Prefix)
                .FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                var _event = _context.Events.Find(eventId);

                var assetEvent = new EventAsset()
                {
                    Title =  _event.Title,
                    CreationDate = DateTime.Now,
                    DeadLine = DateTime.Now,
                    AssetId = asset.Id
                };

                asset.InventNumber = categoryPrefix.Result + generator.Next(0, 1000000).ToString("D6") + asset.InventPrefix;
                asset.SerialNum = serialNum;
                
                asset.IsActive = true;
                if (storage != null)
                {
                    asset.StorageId = storage.Id;
                    asset.EmployeeId = storage.OwnerId;
                }

                if (asset.Image != null)
                {
                    UploadPhoto(asset);
                }
                else
                {
                    asset.ImagePath = "images/default-image.jpg";
                }
                if (asset.Document != null)
                {
                    UploadDocument(asset);
                }
                _context.Add(asset);
                _context.Add(assetEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", asset.SupplierId);
            return View(asset);
        }

        #endregion
        
        #region Edit

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.SingleOrDefaultAsync(m => m.Id == id);

            if (asset == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", asset.SupplierId);
            return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,CategoryId,InventNumber,Date,OfficeId,StorageId,SupplierId,EmployeeId,Image,Id")] Asset asset, string serialNum, string currentPath, string inventNumber)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }
            
            var inventNumSearch = _context.Assets
                .FirstOrDefault(i => i.InventNumber == inventNumber);

            if (inventNumSearch != null)
            {
                ModelState.AddModelError("InventNumber", "Такой номер уже существует");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    asset.SerialNum = serialNum;
                    if (asset.Image != null)
                    {
                        UploadPhoto(asset);
                    }
                    else
                    {
                        asset.ImagePath = currentPath;
                    }

                    asset.InventNumber = inventNumber;
                    asset.IsActive = true;
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", asset.SupplierId);
            return View(asset);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var asset = await _context.Assets.SingleOrDefaultAsync(m => m.Id == id);
            asset.IsActive = false;
            _context.Update(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Move

        public async Task<IActionResult> Move(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.SingleOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", asset.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", asset.SupplierId);
            ViewData["AssetId"] = id;

            var assetsMoveStory = new AssetsMoveStory();
            ViewData["EmployeeFromId"] = new SelectList(_context.Users, "Id", "Login");
            ViewData["OfficeFromId"] = new SelectList(_context.Offices, "Id", "Title");

            ViewData["EmployeeToId"] = new SelectList(_context.Users, "Id", "Login");
            ViewData["OfficeToId"] = new SelectList(_context.Offices, "Id", "Title");



            return View("Move", new MoveViewModel()
            {
                Asset = asset,
                AssetsMoveStory = assetsMoveStory,
                AssetsMoveStories = _context.AssetsMoveStories
                                    .Where(f => f.AssetId == id)
                                    .Include(t => t.EmployeeFrom)
                                    .Include(t => t.OfficeFrom)
                                    .Include(t => t.EmployeeTo)
                                    .Include(t => t.OfficeTo)
            });
            //return View(asset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Move(AssetsMoveStory assetsMoveStory)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetsMoveStory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(assetsMoveStory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeFromId"] = new SelectList(_context.Users, "Id", "Login", assetsMoveStory.EmployeeFromId);
            ViewData["OfficeFromId"] = new SelectList(_context.Offices, "Id", "Title", assetsMoveStory.OfficeFromId);

            ViewData["EmployeeToId"] = new SelectList(_context.Users, "Id", "Login", assetsMoveStory.EmployeeToId);
            ViewData["OfficeToId"] = new SelectList(_context.Offices, "Id", "Title", assetsMoveStory.OfficeToId);
            return View(assetsMoveStory);
        }
        #endregion

        #region AssetExists

        private bool AssetExists(string id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }

        #endregion

        #region UploadPhoto

        private void UploadPhoto(Asset asset)
        {
            var path = Path.Combine(_appEnvironment.WebRootPath, $"images\\{asset.Name}\\image");
            _fileUploadService.Upload(path, asset.Image.FileName, asset.Image);
            asset.ImagePath = $"images/{asset.Name}/image/{asset.Image.FileName}";
        }
        #endregion

        #region UploadDocument

        private void UploadDocument(Asset asset)
        {
            var path = Path.Combine(_appEnvironment.WebRootPath, $"documents\\{asset.Name}\\document");
            _fileUploadService.Upload(path, asset.Document.FileName, asset.Document);
            ///*asset.DocumentPath = $"documents/{asset.Name}/document/{asset.Document.FileName*/}"
        }

        #endregion

        #region Download

        public ActionResult Download(string id)
        {
            var assetA = _context.Assets.FirstOrDefault(x => x.Id == id);

            byte[] b;
            if (assetA.DocumentPath != null)
            {
                b = System.IO.File.ReadAllBytes(assetA.DocumentPath);
                string fileName = "myfile.ext";
                return File(b, MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return View("Error"); ;
            }
        }

        #endregion

        #region Check

        public ActionResult Check(string[] assetId, string officeId)
        {
            bool result = false;
            foreach (var item in assetId)
            {
                var assetIdFind = _context.Assets.FirstOrDefault(a => a.Id == item);
                if (assetIdFind != null)
                {
                    assetIdFind.IsActive = false;
                    assetIdFind.OfficeId = officeId;
                    _context.Update(assetIdFind);
                    _context.SaveChanges();
                }
            }
            result = true;
            return Json(data: new {success = result});
        }

        #endregion

    }
}
