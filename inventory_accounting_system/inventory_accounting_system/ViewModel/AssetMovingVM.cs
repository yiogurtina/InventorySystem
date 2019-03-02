using System.Collections.Generic;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.ViewModel {
    public class AssetMovingVM {
        public AssetsMoveStory AssetsMoveStory { get; set; }
        public IEnumerable<AssetsMoveStory> AssetsMoveStories { get; set; }
        public PageVM PageVM { get; set; }
    }
}