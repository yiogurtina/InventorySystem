using System.Collections.Generic;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.Reposytory
{
    public class CategoryReposytory : ICategoryReposytory
    {
        private ApplicationDbContext context;

        public CategoryReposytory(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }
    }
}