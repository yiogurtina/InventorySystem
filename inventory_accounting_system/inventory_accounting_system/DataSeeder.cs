using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using inventory_accounting_system.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system {
    public class DataSeeder {
        public static void Seed (ApplicationDbContext context) {

            if (!context.Categories.Any ()) {
                var categories = new List<Category> {
                    new Category { Id = "d4416522-6c60-4f39-9412-e7c3d516af44", Name = "Компьютеры", Prefix = "001" },
                    new Category { Id = "5a0dedc3-1685-49cf-ab4f-10409c00be8c", Name = "Кондиционеры", Prefix = "002" },
                    new Category { Id = "fac89fd8-3290-4029-90db-752729910a8a", Name = "Мебель", Prefix = "003" },
                    new Category { Id = "25ef8274-88b4-49f3-888d-1c53d5ea03ec", Name = "Авто", Prefix = "004" }
                };
                context.AddRange (categories);
                context.SaveChanges ();
            }

            if (!context.Events.Any ()) {
                var events = new List<Event> {
                    new Event {
                    Id = "be44a607-c73b-4757-8cfb-e8d50758aac1",
                    Title = "Замена масла", CategoryId = "25ef8274-88b4-49f3-888d-1c53d5ea03ec",
                    Content = "Нужно заменить масло", Periodicity = "Ежедневно"
                    },
                    new Event {
                    Id = "a895e0e8-b52d-43f1-8dec-6a2f92680de4",
                    Title = "Замена фриона", CategoryId = "5a0dedc3-1685-49cf-ab4f-10409c00be8c",
                    Content = "Нужно заменить фрион", Periodicity = "Ежегодно"
                    },
                };
                context.AddRange (events);
                context.SaveChanges ();
            }

            if (!context.Suppliers.Any ()) {
                var suppliers = new List<Supplier> {
                    new Supplier { Id = "77326e6e-ea89-4218-8538-381afa239619", Name = "Самсунг" },
                    new Supplier { Id = "667138df-5a48-42f1-b092-91465566cf17", Name = "Intel" },
                    new Supplier { Id = "eded7137-28d7-444b-8d61-a58322e2dff6", Name = "Panasonic" },
                    new Supplier { Id = "e38e288f-8bea-4cf6-a55f-8d7d111fa244", Name = "ОАО Мебель" },
                    new Supplier { Id = "c38807b6-8d33-40da-86de-a24124e2827a", Name = "Sony" },
                    new Supplier { Id = "bcbec3e6-8a98-405c-ad5e-b5ed455a949b", Name = "AutoMotors" }

                };
                context.AddRange (suppliers);
                context.SaveChanges ();
            }

            if (!context.Offices.Any (b => b.IsMain)) {
                var storages = new List<Office> {
                new Office { Id = "3650a05e-449c-4853-b19d-0a5680173395", Title = "Главный склад", IsMain = true },
                new Office { Id = "f40af8f6-e0db-47b8-908f-faf14ebb4d35", Title = "Офис 1", IsMain = false },
                new Office { Id = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f", Title = "Офис 2", IsMain = false },
                new Office { Id = "75861c3b-0b03-40b5-96a3-ed702321333d", Title = "Офис 3", IsMain = false },
                new Office { Id = "9f4d1107-7425-422e-8109-12e4d29b776f", Title = "Офис 4", IsMain = false }

                };
                context.AddRange (storages);
                context.SaveChanges ();
            }

            if (context.Users.Any (u => u.Name != "admin")) {
                var emp = new List<Employee> {
                new Employee {
                Id = "a708b56f-1bbb-4011-a986-3a4ed54c27e4",
                Login = "Linda", Name = "Линда", Surname = "Кулич",
                IsDelete = false, PhoneNumber = "+996 (705) 253-569", UserName = "Linda",
                Email = "linda@linda.com",
                LockoutEnabled = true,
                PasswordHash = "MTIzVDMyMSE=", // 123T321!
                SecurityStamp = "3c6093a0-5857-42e4-a98b-485d35dc0a71",
                NormalizedEmail = "LINDA@LINDA.COM",
                NormalizedUserName = "LINDA",
                OfficeId = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f"
                },
                new Employee {
                Id = "33152f23-7275-4caa-94b2-abcf68749e65",
                Login = "Linda", Name = "Инна", Surname = "Шербакова",
                IsDelete = false, PhoneNumber = "+996 (501) 111-444", UserName = "Inna",
                Email = "inna@inna.com",
                LockoutEnabled = true,
                PasswordHash = "MTIzVDMyMSE=", // 123T321!
                SecurityStamp = "3c6093a0-5857-42e4-a98b-485d35dc0a71",
                NormalizedEmail = "INNA@INNA.COM",
                NormalizedUserName = "INNA",
                OfficeId = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f"
                },
                new Employee {
                Id = "138b03e6-00bb-41a3-a911-5b6dba29ed40",
                Login = "Boris", Name = "Борис", Surname = "Джонсон",
                IsDelete = false, PhoneNumber = "+996 (777) 111-444", UserName = "Boris",
                Email = "boris@boris.com",
                LockoutEnabled = true,
                PasswordHash = "MTIzVDMyMSE=", // 123T321!
                SecurityStamp = "cae67278-6859-42fc-951d-c3a2b3bcbcba",
                NormalizedEmail = "BORIS@BORIS.COM",
                NormalizedUserName = "BORIS",
                OfficeId = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f"
                },
                };
                context.AddRange (emp);
                context.SaveChanges ();
            }

            if (!context.Assets.Any ()) {

                var storages = new List<Asset> {
                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/383548201_w640_h640_stul-kreslo.jpg",
                    InStock = true, InventNumber = "1607368420", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    Id = "9e9da958-0bb9-44fe-9ea3-c9045a27631b",
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "a708b56f-1bbb-4011-a986-3a4ed54c27e4", ImagePath = "images/Стул/image/383548201_w640_h640_stul-kreslo.jpg",
                    InStock = false, InventNumber = "1729841315", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/383548201_w640_h640_stul-kreslo.jpg",
                    InStock = true, InventNumber = "1293621740", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    Id = "fffc231d-e301-4958-8d77-ea369342ac79",
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "a708b56f-1bbb-4011-a986-3a4ed54c27e4", ImagePath = "images/Стул/image/383548201_w640_h640_stul-kreslo.jpg",
                    InStock = false, InventNumber = "2603856835", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2017-07-23 03:33:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/images.jpg",
                    InStock = true, InventNumber = "2448709460", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2017-07-23 03:33:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/images.jpg",
                    InStock = true, InventNumber = "3640651107", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2017-07-23 03:33:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/images.jpg",
                    InStock = true, InventNumber = "3016654865", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2017-07-23 03:33:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/dizaynerskie-stulya.jpg",
                    InStock = true, InventNumber = "839616018", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2019-02-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/HP X360 Convertible/image/x360-11-blanc-7 white_5746.jpg",
                    InStock = true, InventNumber = "1143377576", InventPrefix = 0, IsActive = true,
                    Name = "HP X360 Convertible", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 500, SerialNum = "4142557477", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2018-02-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/HP X360 Convertible/image/x360-11-blanc-7 white_5746.jpg",
                    InStock = true, InventNumber = "727865014", InventPrefix = 0, IsActive = true,
                    Name = "HP X360 Convertible", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 500, SerialNum = "2142393175", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2018-02-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/HP X360 Convertible/image/x360-11-blanc-7 white_5746.jpg",
                    InStock = true, InventNumber = "39065402", InventPrefix = 0, IsActive = true,
                    Name = "HP X360 Convertible", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 500, SerialNum = "90123111", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    Id = "1760dbaa-ec9e-4454-a857-a8a3ab9b935a",
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2013-01-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/Acer Swift 3 SF314-54 Lava/image/sf314-54 lava red_4167.jpg",
                    InStock = true, InventNumber = "3633238348", InventPrefix = 0, IsActive = true,
                    Name = "Acer Swift 3 SF314-54 Lava", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 800, SerialNum = "2238140849", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2013-01-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/Acer Swift 3 SF314-54 Lava/image/sf314-54 lava red_4167.jpg",
                    InStock = true, InventNumber = "3417027142", InventPrefix = 0, IsActive = true,
                    Name = "Acer Swift 3 SF314-54 Lava", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 800, SerialNum = "2996980874", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2014-11-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/Acer Swift 3 SF314-54/image/sf314  blue_1067.jpg",
                    InStock = true, InventNumber = "1481468430", InventPrefix = 0, IsActive = true,
                    Name = "Acer Swift 3 SF314-54", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 800, SerialNum = "3018317335", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2014-11-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/Acer Swift 3 SF314-54/image/sf314  blue_1067.jpg",
                    InStock = true, InventNumber = "1251161730", InventPrefix = 0, IsActive = true,
                    Name = "Acer Swift 3 SF314-54", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 800, SerialNum = "3351644419", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2014-11-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/Acer Swift 3 SF314-54/image/sf314  blue_1067.jpg",
                    InStock = true, InventNumber = "2756363584", InventPrefix = 0, IsActive = true,
                    Name = "Acer Swift 3 SF314-54", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 800, SerialNum = "2142287995", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "5a0dedc3-1685-49cf-ab4f-10409c00be8c", Date = DateTime.Parse ("2017-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/TCL TAC-18CHSHS/HS/image/copy_gree_gwh09na_k3nnd2f_komfort_58b01d4426c8a_images_1868533376.jpg",
                    InStock = true, InventNumber = "842232992", InventPrefix = 0, IsActive = true,
                    Name = "TCL TAC-18CHS/HS", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 350, SerialNum = null, StatusMovingAssets = null, SupplierId = "eded7137-28d7-444b-8d61-a58322e2dff6"
                    },

                    new Asset {
                    CategoryId = "5a0dedc3-1685-49cf-ab4f-10409c00be8c", Date = DateTime.Parse ("2017-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/TCL TAC-18CHSHS/HS/image/copy_gree_gwh09na_k3nnd2f_komfort_58b01d4426c8a_images_1868533376.jpg",
                    InStock = true, InventNumber = "2747423109", InventPrefix = 0, IsActive = true,
                    Name = "TCL TAC-18CHS/HS", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 350, SerialNum = null, StatusMovingAssets = null, SupplierId = "eded7137-28d7-444b-8d61-a58322e2dff6"
                    },

                    new Asset {
                    CategoryId = "5a0dedc3-1685-49cf-ab4f-10409c00be8c", Date = DateTime.Parse ("2017-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/TCL TAC-18CHSHS/HS/image/copy_gree_gwh09na_k3nnd2f_komfort_58b01d4426c8a_images_1868533376.jpg",
                    InStock = true, InventNumber = "3345524500", InventPrefix = 0, IsActive = true,
                    Name = "TCL TAC-18CHS/HS", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 350, SerialNum = null, StatusMovingAssets = null, SupplierId = "eded7137-28d7-444b-8d61-a58322e2dff6"
                    },

                    new Asset {
                    CategoryId = "25ef8274-88b4-49f3-888d-1c53d5ea03ec", Date = DateTime.Parse ("2018-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/2012 Mercedes-Benz E-Class Sedan 4D E550/image/2012MEB005a_640_05.jpg",
                    InStock = true, InventNumber = "1729474494", InventPrefix = 0, IsActive = true,
                    Name = "Mercedes-Benz E-Class Sedan 4D E550", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 135000, SerialNum = null, StatusMovingAssets = null, SupplierId = "bcbec3e6-8a98-405c-ad5e-b5ed455a949b"
                    },

                    new Asset {
                    Id = "4784ee6d-d002-4dc8-ae9b-3bb0128f8fa4",
                    CategoryId = "25ef8274-88b4-49f3-888d-1c53d5ea03ec", Date = DateTime.Parse ("2018-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/2012 Mercedes-Benz E-Class Sedan 4D E550/image/images.jpg",
                    InStock = true, InventNumber = "4242940117", InventPrefix = 0, IsActive = true,
                    Name = "Mercedes-Benz E-Class Sedan 4D E550", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 135000, SerialNum = null, StatusMovingAssets = null, SupplierId = "bcbec3e6-8a98-405c-ad5e-b5ed455a949b"
                    },

                };
                context.AddRange (storages);
                context.SaveChanges ();

                if (!context.AssetsMoveStories.Any ()) {
                    var move = new List<AssetsMoveStory> {
                        new AssetsMoveStory {
                        Id = "b5a22436-c35a-444a-94e0-4829b73ca92a",
                        AssetId = "fffc231d-e301-4958-8d77-ea369342ac79",
                        DateCurrent = DateTime.Parse ("2019-02-22 18:43:12.1266610"),
                        DateEnd = DateTime.Parse ("2100-01-01 00:00:00.0000000"),
                        DateStart = DateTime.Parse ("2019-02-22 00:00:00.0000000"),
                        EmployeeFromId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                        EmployeeToId = "a708b56f-1bbb-4011-a986-3a4ed54c27e4",
                        OfficeFromId = "3650a05e-449c-4853-b19d-0a5680173395",
                        OfficeToId = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f",
                        StatusMovinHistory = "передан в офис"
                        },
                        new AssetsMoveStory {
                        Id = "2fa8cef3-07a5-4045-bc5c-433bb058f323",
                        AssetId = "9e9da958-0bb9-44fe-9ea3-c9045a27631b",
                        DateCurrent = DateTime.Parse ("2019-02-22 18:42:27.9155905"),
                        DateEnd = DateTime.Parse ("2100-01-01 00:00:00.0000000"),
                        DateStart = DateTime.Parse ("2019-02-22 00:00:00.0000000"),
                        EmployeeFromId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                        EmployeeToId = "a708b56f-1bbb-4011-a986-3a4ed54c27e4",
                        OfficeFromId = "3650a05e-449c-4853-b19d-0a5680173395",
                        OfficeToId = "952be3a3-1b20-4bc2-a6e2-a9738ddc771f",
                        StatusMovinHistory = "передан в офис"
                        },
                    };
                    context.AddRange (move);
                    context.SaveChanges ();
                }
            }

        }
    }

}

public static class IdentityDataInit {
    public static void SeedData (
        UserManager<Employee> userManager,
        RoleManager<IdentityRole> roleManager) {
        SeedRoles (roleManager);
        SeedUsers (userManager);

    }

    public static void SeedUsers (UserManager<Employee> userManager) {

        if (userManager.FindByNameAsync ("admin").Result == null) {
            Employee user = new Employee () {
            Id = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
            Name = "admin",
            Surname = "admin",
            UserName = "admin",
            Email = "admin@admin.com",
            Login = "admin"
            };

            var result = userManager.CreateAsync (user, "admin").Result; //Password

            if (result.Succeeded) {
                userManager.AddToRoleAsync (user, "admin").Wait ();
            }
        }

        if (userManager.FindByNameAsync ("manager").Result == null) {
            Employee user = new Employee () {
            Id = "ed96ce08-556a-4c62-b91c-ce8111c15fea",
            Name = "manager",
            Surname = "manager",
            UserName = "manager",
            Email = "manager@manager.com",
            Login = "manager"
            };

            var result = userManager.CreateAsync (user, "manager").Result; //Password

            if (result.Succeeded) {
                userManager.AddToRoleAsync (user, "manager").Wait ();
            }
        }

        if (userManager.FindByNameAsync ("user").Result == null) {
            Employee user = new Employee () {
            Id = "7d6f6f42-c262-4070-9a60-41ce5229a086",
            Name = "user",
            Surname = "user",
            UserName = "user",
            Email = "user@user.com",
            Login = "user"
            };

            var result = userManager.CreateAsync (user, "user").Result; //Password

            if (result.Succeeded) {
                userManager.AddToRoleAsync (user, "user").Wait ();
            }
        }

    }

    public static void SeedRoles
        (RoleManager<IdentityRole> roleManager) {
            if (!roleManager.RoleExistsAsync ("Admin").Result) {
                IdentityRole role = new IdentityRole ();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync (role).Result;
            }

            if (!roleManager.RoleExistsAsync ("Manager").Result) {
                IdentityRole role = new IdentityRole ();
                role.Name = "Manager";
                IdentityResult roleResult = roleManager.CreateAsync (role).Result;
            }

            if (!roleManager.RoleExistsAsync ("User").Result) {
                IdentityRole role = new IdentityRole ();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync (role).Result;
            }
        }

}