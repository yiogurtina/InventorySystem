using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
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

            // if (!context.Events.Any ()) {
            //     var categories = new List<Event> {
            //         new Event { Id = "f96fe2f4-9649-4e31-9895-aa91f398a1aa", 
            //         Title = "Замена масла", CategoryId = "25ef8274-88b4-49f3-888d-1c53d5ea03ec" ,
            //         Content = "Замените масло", Periodicity = "Ежудневно"},
            //     };
            //     context.AddRange (categories);
            //     context.SaveChanges ();
            // }

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

            if (!context.Assets.Any ()) {

                DateTime dateseed = DateTime.Parse ("2019-02-22 09:39:43.2914501");
                var storages = new List<Asset> {
                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/5a857f77169b8cbe571974f98c2eb809.jpg",
                    InStock = true, InventNumber = "1607368420", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/5a857f77169b8cbe571974f98c2eb809.jpg",
                    InStock = true, InventNumber = "1729841315", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/5a857f77169b8cbe571974f98c2eb809.jpg",
                    InStock = true, InventNumber = "1293621740", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 37, SerialNum = null, StatusMovingAssets = null, SupplierId = "e38e288f-8bea-4cf6-a55f-8d7d111fa244"
                    },

                    new Asset {
                    CategoryId = "fac89fd8-3290-4029-90db-752729910a8a", Date = DateTime.Parse ("2019-02-22 09:39:43.2914501"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/5a857f77169b8cbe571974f98c2eb809.jpg",
                    InStock = true, InventNumber = "2603856835", InventPrefix = 0, IsActive = true, Name = "Стул", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
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
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb", ImagePath = "images/Стул/image/Вегас_темн. графит 226.jpg",
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
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2013-01-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/Acer Swift 3 SF314-54 Lava/image/sf314-54 lava red_4167 (1).jpg",
                    InStock = true, InventNumber = "3633238348", InventPrefix = 0, IsActive = true,
                    Name = "Acer Swift 3 SF314-54 Lava", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 800, SerialNum = "2238140849", StatusMovingAssets = null, SupplierId = "667138df-5a48-42f1-b092-91465566cf17"
                    },

                    new Asset {
                    CategoryId = "d4416522-6c60-4f39-9412-e7c3d516af44", Date = DateTime.Parse ("2013-01-22 10:13:24.0116480"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/Acer Swift 3 SF314-54 Lava/image/sf314-54 lava red_4167 (1).jpg",
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
                    ImagePath = "images/TCL TAC-18CHS/HS/image/kondicioner-nastennyj-tcl-tac-18chshs-na-50-55-m2-installjacija-2.jpg",
                    InStock = true, InventNumber = "842232992", InventPrefix = 0, IsActive = true,
                    Name = "TCL TAC-18CHS/HS", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 350, SerialNum = "2142287995", StatusMovingAssets = null, SupplierId = "eded7137-28d7-444b-8d61-a58322e2dff6"
                    },

                    new Asset {
                    CategoryId = "5a0dedc3-1685-49cf-ab4f-10409c00be8c", Date = DateTime.Parse ("2017-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/TCL TAC-18CHS/HS/image/kondicioner-nastennyj-tcl-tac-18chshs-na-50-55-m2-installjacija-2.jpg",
                    InStock = true, InventNumber = "2747423109", InventPrefix = 0, IsActive = true,
                    Name = "TCL TAC-18CHS/HS", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 350, SerialNum = "82476851", StatusMovingAssets = null, SupplierId = "eded7137-28d7-444b-8d61-a58322e2dff6"
                    },

                    new Asset {
                    CategoryId = "5a0dedc3-1685-49cf-ab4f-10409c00be8c", Date = DateTime.Parse ("2017-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/TCL TAC-18CHS/HS/image/kondicioner-nastennyj-tcl-tac-18chshs-na-50-55-m2-installjacija-2.jpg",
                    InStock = true, InventNumber = "3345524500", InventPrefix = 0, IsActive = true,
                    Name = "TCL TAC-18CHS/HS", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 350, SerialNum = "869326795", StatusMovingAssets = null, SupplierId = "eded7137-28d7-444b-8d61-a58322e2dff6"
                    },

                    new Asset {
                    CategoryId = "25ef8274-88b4-49f3-888d-1c53d5ea03ec", Date = DateTime.Parse ("2018-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/ГАЗ-12 ЗИМ (Гиперлайнер)/image/12325043.jpg",
                    InStock = true, InventNumber = "1729474494", InventPrefix = 0, IsActive = true,
                    Name = "ГАЗ-12 ЗИМ (Гиперлайнер)", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 135000, SerialNum = "4268162160", StatusMovingAssets = null, SupplierId = "bcbec3e6-8a98-405c-ad5e-b5ed455a949b"
                    },

                    new Asset {
                    CategoryId = "25ef8274-88b4-49f3-888d-1c53d5ea03ec", Date = DateTime.Parse ("2018-10-25 10:35:00.3621110"),
                    DocumentPath = null, EmployeeId = "8a3ca0f6-fb50-4e23-8dc8-554898fa5ddb",
                    ImagePath = "images/ГАЗ-12 ЗИМ (Гиперлайнер)/image/12325043.jpg",
                    InStock = true, InventNumber = "4242940117", InventPrefix = 0, IsActive = true,
                    Name = "ГАЗ-12 ЗИМ (Гиперлайнер)", OfficeId = "3650a05e-449c-4853-b19d-0a5680173395",
                    Price = 135000, SerialNum = "3810738204", StatusMovingAssets = null, SupplierId = "bcbec3e6-8a98-405c-ad5e-b5ed455a949b"
                    },

                };
                context.AddRange (storages);
                context.SaveChanges ();
            }
        }

        public static void SeedUsersOffices (UserManager<Employee> userManager) {

            // Office 3
            if (userManager.FindByNameAsync ("manager").Result == null) {
                Employee manager = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "manager3",
                Surname = "manager3",
                UserName = "manager3",
                Email = "manager3@manager3.com",
                Login = "manager3",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (manager, "manager3").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (manager, "manager").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("user").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "user",
                Surname = "user",
                UserName = "user",
                Email = "user@user.com",
                Login = "user",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "user").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("linda").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Линда",
                Surname = "Васильевна",
                UserName = "Кулич",
                Email = "linda@linda.com",
                Login = "Linda",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "linda").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("don").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Дмитрий",
                Surname = "Васильевич",
                UserName = "Кружкин",
                Email = "don@don.com",
                Login = "Don",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "don").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("igor").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Игорь",
                Surname = "Васильевич",
                UserName = "Кнут",
                Email = "igor@igor.com",
                Login = "Igor",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "igor").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("igor2").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Игорь2",
                Surname = "Васильевич",
                UserName = "Кнут",
                Email = "igor2@igor.com",
                Login = "Igor2",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "igor2").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("igor3").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Игорь3",
                Surname = "Васильевич",
                UserName = "Кнут",
                Email = "igor3@igor.com",
                Login = "Igor3",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "igor3").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("igor4").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Игорь4",
                Surname = "Васильевич",
                UserName = "Кнут",
                Email = "igor4@igor.com",
                Login = "Igor4",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "igor4").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("igor5").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Игорь5",
                Surname = "Васильевич",
                UserName = "Кнут",
                Email = "igor5@igor.com",
                Login = "Igor5",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "igor5").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("igor6").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Игорь6",
                Surname = "Васильевич",
                UserName = "Кнут",
                Email = "igor6@igor.com",
                Login = "Igor6",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "igor6").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("igor7").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Игорь7",
                Surname = "Васильевич",
                UserName = "Кнут",
                Email = "igor7@igor.com",
                Login = "Igor7",
                OfficeId = "75861c3b-0b03-40b5-96a3-ed702321333d"
                };

                var result = userManager.CreateAsync (user, "igor7").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            //Office 1 
            if (userManager.FindByNameAsync ("manager1").Result == null) {
                Employee manager = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "manager1",
                Surname = "manager1",
                UserName = "manager1",
                Email = "manager1@manager1.com",
                Login = "manager1",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (manager, "manager").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (manager, "manager").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("boris").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "boris",
                Surname = "boris",
                UserName = "boris",
                Email = "boris@boris.com",
                Login = "boris",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (user, "boris").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("boris2").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "boris2",
                Surname = "boris2",
                UserName = "boris2",
                Email = "boris2@boris2.com",
                Login = "boris2",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (user, "boris2").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("boris3").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "boris3",
                Surname = "boris3",
                UserName = "boris3",
                Email = "boris3@boris3.com",
                Login = "boris3",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (user, "boris3").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("boris4").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "boris4",
                Surname = "boris4",
                UserName = "boris4",
                Email = "boris4@boris4.com",
                Login = "boris4",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (user, "boris4").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("boris5").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Борис",
                Surname = "Григорьевич",
                UserName = "Дулькин",
                Email = "boris5@boris5.com",
                Login = "boris5",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (user, "boris5").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("boris6").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Борис",
                Surname = "Григорьевич",
                UserName = "Булькин",
                Email = "boris6@boris6.com",
                Login = "boris6",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (user, "boris6").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
                }
            }

            if (userManager.FindByNameAsync ("boris7").Result == null) {
                Employee user = new Employee () {
                Id = Guid.NewGuid ().ToString (),
                Name = "Борис",
                Surname = "Григорьевич",
                UserName = "Федунькин",
                Email = "boris7@boris7.com",
                Login = "boris7",
                OfficeId = "f40af8f6-e0db-47b8-908f-faf14ebb4d35"
                };

                var result = userManager.CreateAsync (user, "boris7").Result; //Password

                if (result.Succeeded) {
                    userManager.AddToRoleAsync (user, "user").Wait ();
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
}