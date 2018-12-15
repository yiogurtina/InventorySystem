using System;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory_accounting_system.Models
{
    public class AssetVM : ViewComponent
    {
        Asset asset = new Asset();
        Category category = new Category();
        static Random generator = new Random();
        private ApplicationDbContext _context;

        public AssetVM(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
//            var categoryPrefix = _context.Assets.Any(c => c.CategoryId);

            if (!AddUserExists(asset.Id))
            {
                var result = new Asset()
                {
                    InventNumber = generator.Next(0, 1000000).ToString("D6")
                };
                _context.Assets.Add(result);
                await _context.SaveChangesAsync();
                return View(result);
               
            }

            return View(_context.Assets.FirstOrDefault());
        }

        private bool AddUserExists(string id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}