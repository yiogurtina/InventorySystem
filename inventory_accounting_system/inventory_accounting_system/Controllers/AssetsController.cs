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
using Microsoft.AspNetCore.Hosting;
using inventory_accounting_system.Services;
using Microsoft.AspNetCore.Http;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

        public async Task<IActionResult> Index()
        {
            var assets = _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .Where(a => a.IsActive == true);
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

        #region Create

        public IActionResult Create()
        {
            string usrId =_userManager.GetUserId(User);
            var user = _context.Users.Find(usrId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryId,InventNumber,InventPrefix,Date,OfficeId,StorageId,SupplierId,EmployeeId,Id,Image, Document")] Asset asset, string serialNum)
        {
            var categoryPrefix = _context.Categories
                .Where(c => c.Id == asset.CategoryId)
                .Select(c => c.Prefix)
                .FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                asset.InventNumber = categoryPrefix.Result + generator.Next(0, 1000000).ToString("D6") + asset.InventPrefix;
                asset.SerialNum = serialNum;
                asset.IsActive = true;
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
    }
}
