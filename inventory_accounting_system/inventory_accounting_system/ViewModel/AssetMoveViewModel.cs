using inventory_accounting_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.ViewModel
{
    public class MoveViewModel
    {
        public Asset Asset { get; set; }
        public AssetsMoveStory AssetsMoveStory { get; set; }
        public IEnumerable<AssetsMoveStory> AssetsMoveStories { get; set; }
    }
}
