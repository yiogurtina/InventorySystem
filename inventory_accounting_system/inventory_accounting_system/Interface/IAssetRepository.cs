using inventory_accounting_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Interface
{
    public interface IAssetRepository
    {
        IEnumerable<Asset> GetAssets();
    }
}
