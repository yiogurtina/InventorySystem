﻿// <auto-generated />
using inventory_accounting_system.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace inventory_accounting_system.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("inventory_accounting_system.Models.Asset", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("DocumentPath");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("ImagePath");

                    b.Property<bool>("InStock");

                    b.Property<string>("InventNumber");

                    b.Property<int>("InventPrefix");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("OfficeId");

                    b.Property<string>("SerialNum");

                    b.Property<string>("SupplierId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OfficeId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.AssetOnStorage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetId");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("StorageId");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StorageId");

                    b.ToTable("AssetOnStorages");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.AssetsMoveStory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetId");

                    b.Property<DateTime>("DateCurrent");

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateStart");

                    b.Property<string>("EmployeeFromId");

                    b.Property<string>("EmployeeToId");

                    b.Property<string>("OfficeFromId");

                    b.Property<string>("OfficeToId");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("EmployeeFromId");

                    b.HasIndex("EmployeeToId");

                    b.HasIndex("OfficeFromId");

                    b.HasIndex("OfficeToId");

                    b.ToTable("AssetsMoveStories");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Number");

                    b.Property<string>("OfficeId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("OfficeId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryId");

                    b.Property<string>("Content");

                    b.Property<string>("Periodicity");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.EventAsset", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("DeadLine");

                    b.Property<string>("EmployeeId");

                    b.Property<string>("EventId");

                    b.Property<int>("Period");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EventId");

                    b.ToTable("AssetEvents");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.InventoryNumberHistory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetIdCreate");

                    b.Property<string>("Become");

                    b.Property<string>("Been");

                    b.Property<DateTime?>("ChangeDate");

                    b.Property<DateTime>("CreateDate");

                    b.HasKey("Id");

                    b.ToTable("InventoryNumberHistories");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Office", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMain");

                    b.Property<string>("StorageId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.OrderEmployee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime?>("DateTo");

                    b.Property<string>("EmployeeFromId");

                    b.Property<string>("EmployeeToId");

                    b.Property<string>("OfficeId");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeFromId");

                    b.HasIndex("EmployeeToId");

                    b.HasIndex("OfficeId");

                    b.ToTable("OrderEmployees");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.OrderEmployeeAdmin", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentAdmin");

                    b.Property<string>("ContentUser");

                    b.Property<DateTime>("DateFromAdmin");

                    b.Property<DateTime?>("DateToAdmin");

                    b.Property<string>("EmployeeFromAdminId");

                    b.Property<string>("EmployeeToAdminId");

                    b.Property<string>("OfficeAdminId");

                    b.Property<string>("OrderEmployeeId");

                    b.Property<string>("StatusAdmin");

                    b.Property<string>("TitleAdmin");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeFromAdminId");

                    b.HasIndex("EmployeeToAdminId");

                    b.HasIndex("OfficeAdminId");

                    b.HasIndex("OrderEmployeeId");

                    b.ToTable("OrderEmployeeAdmins");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Storage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMain");

                    b.Property<string>("Name");

                    b.Property<string>("OfficeId");

                    b.Property<string>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId")
                        .IsUnique()
                        .HasFilter("[OfficeId] IS NOT NULL");

                    b.HasIndex("OwnerId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Supplier", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Asset", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("inventory_accounting_system.Models.Employee", "Employee")
                        .WithMany("Assets")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("inventory_accounting_system.Models.Office", "Office")
                        .WithMany("Assets")
                        .HasForeignKey("OfficeId");

                    b.HasOne("inventory_accounting_system.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.AssetOnStorage", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Asset", "Asset")
                        .WithMany()
                        .HasForeignKey("AssetId");

                    b.HasOne("inventory_accounting_system.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("inventory_accounting_system.Models.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.AssetsMoveStory", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Asset", "Asset")
                        .WithMany("AssetsMoveStories")
                        .HasForeignKey("AssetId");

                    b.HasOne("inventory_accounting_system.Models.Employee", "EmployeeFrom")
                        .WithMany("assetsMoveStoriesFrom")
                        .HasForeignKey("EmployeeFromId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("inventory_accounting_system.Models.Employee", "EmployeeTo")
                        .WithMany("assetsMoveStoriesTo")
                        .HasForeignKey("EmployeeToId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("inventory_accounting_system.Models.Office", "OfficeFrom")
                        .WithMany("assetsMoveStoriesFrom")
                        .HasForeignKey("OfficeFromId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("inventory_accounting_system.Models.Office", "OfficeTo")
                        .WithMany("assetsMoveStoriesTo")
                        .HasForeignKey("OfficeToId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Employee", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Office", "Office")
                        .WithMany("Employees")
                        .HasForeignKey("OfficeId");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Event", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Category", "Category")
                        .WithMany("Events")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.EventAsset", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Asset", "Asset")
                        .WithMany("AssetEvents")
                        .HasForeignKey("AssetId");

                    b.HasOne("inventory_accounting_system.Models.Employee", "Employee")
                        .WithMany("EventAssets")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("inventory_accounting_system.Models.Event", "Event")
                        .WithMany("EventAssets")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.OrderEmployee", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Employee", "EmployeeFrom")
                        .WithMany()
                        .HasForeignKey("EmployeeFromId");

                    b.HasOne("inventory_accounting_system.Models.Employee", "EmployeeTo")
                        .WithMany()
                        .HasForeignKey("EmployeeToId");

                    b.HasOne("inventory_accounting_system.Models.Office", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeId");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.OrderEmployeeAdmin", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Employee", "EmployeeFromAdmin")
                        .WithMany()
                        .HasForeignKey("EmployeeFromAdminId");

                    b.HasOne("inventory_accounting_system.Models.Employee", "EmployeeToAdmin")
                        .WithMany()
                        .HasForeignKey("EmployeeToAdminId");

                    b.HasOne("inventory_accounting_system.Models.Office", "OfficeAdmin")
                        .WithMany()
                        .HasForeignKey("OfficeAdminId");

                    b.HasOne("inventory_accounting_system.Models.OrderEmployee", "OrderEmployee")
                        .WithMany()
                        .HasForeignKey("OrderEmployeeId");
                });

            modelBuilder.Entity("inventory_accounting_system.Models.Storage", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Office", "Office")
                        .WithOne("Storage")
                        .HasForeignKey("inventory_accounting_system.Models.Storage", "OfficeId");

                    b.HasOne("inventory_accounting_system.Models.Employee", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("inventory_accounting_system.Models.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("inventory_accounting_system.Models.Employee")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
