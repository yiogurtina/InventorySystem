using System.Collections.Generic;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.Reposytory
{
    public class CategoryReposytory : ICategoryReposytory
    {
        private ApplicationDbContext _context;

        public IEnumerable<Category> Categories
        {
            get { return _context.Categories; }
        }
    }
}