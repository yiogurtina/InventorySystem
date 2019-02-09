using System;
using System.Collections.Generic;
using System.Linq;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.ViewModel {

    public class IndexVM {

        public Asset Asset { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public PageVM PageVM { get; set; }

    }

}