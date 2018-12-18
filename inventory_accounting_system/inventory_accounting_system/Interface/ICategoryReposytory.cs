using System.Collections.Generic;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.Interface
{
    public interface ICategoryReposytory
    {
        IEnumerable<Category> Categories { get; }
    }
}