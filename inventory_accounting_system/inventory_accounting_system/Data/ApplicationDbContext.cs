using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetsMoveStory> AssetsMoveStories { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<AssetOnStorage> AssetOnStorages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventAsset> AssetEvents { get; set; }
        public DbSet<OrderEmployee> OrderEmployees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

    }
}
