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
        public DbSet<OrderEmployeeAdmin> OrderEmployeeAdmins { get; set; }
        public DbSet<InventoryNumberHistory> InventoryNumberHistories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssetsMoveStory>()
              .HasOne(p => p.OfficeFrom)
              .WithMany(p => p.assetsMoveStoriesFrom)
              .HasForeignKey(p => p.OfficeFromId);

            modelBuilder.Entity<Office>()
                .HasMany(c => c.assetsMoveStoriesFrom)
                .WithOne(c => c.OfficeFrom)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetsMoveStory>()
              .HasOne(p => p.OfficeTo)
              .WithMany(p => p.assetsMoveStoriesTo)
              .HasForeignKey(p => p.OfficeToId);

            modelBuilder.Entity<Office>()
                .HasMany(c => c.assetsMoveStoriesTo)
                .WithOne(c => c.OfficeTo)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<AssetsMoveStory>()
              .HasOne(p => p.EmployeeFrom)
              .WithMany(p => p.assetsMoveStoriesFrom)
              .HasForeignKey(p => p.EmployeeFromId);

            modelBuilder.Entity<Employee>()
                .HasMany(c => c.assetsMoveStoriesFrom)
                .WithOne(c => c.EmployeeFrom)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetsMoveStory>()
              .HasOne(p => p.EmployeeTo)
              .WithMany(p => p.assetsMoveStoriesTo)
              .HasForeignKey(p => p.EmployeeToId);

            modelBuilder.Entity<Employee>()
                .HasMany(c => c.assetsMoveStoriesTo)
                .WithOne(c => c.EmployeeTo)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
