using System;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using Microsoft.AspNetCore.Mvc;

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
            if (!AddUserExists(asset.Id))
            {
                return View(new Asset()
                {
                    InventNumber = category.Prefix + generator.Next(0, 1000000).ToString("D6") + asset.InventPrefix,
                    
                });
            }
            return View(_context.Assets.FirstOrDefault());
        }

        private bool AddUserExists(string id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}