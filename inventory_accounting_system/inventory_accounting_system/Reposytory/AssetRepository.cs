using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.Reposytory
{
    public class AssetRepository : IAssetRepository
    {
        private ApplicationDbContext context;

        public AssetRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Asset> GetAssets()
        {
            return context.Assets;
        }
    }
}
