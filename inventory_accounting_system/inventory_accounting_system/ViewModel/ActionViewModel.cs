using inventory_accounting_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetAction = inventory_accounting_system.Models.AssetAction;

namespace inventory_accounting_system.ViewModel
{
    public class ActionMoveViewModel
    {
        public Asset Asset { get; set; }
        public AssetAction AssetAction { get; set; }
        public IEnumerable<AssetAction> AssetActions { get; set; }
    }
}
