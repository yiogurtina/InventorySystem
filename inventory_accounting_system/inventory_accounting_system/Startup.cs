using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Hubs;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;
using inventory_accounting_system.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace inventory_accounting_system {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));

            services.AddIdentity<Employee, IdentityRole> (options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 3;
                })
                .AddEntityFrameworkStores<ApplicationDbContext> ()
                .AddDefaultTokenProviders ();

            services.AddTransient<IEmailSender, EmailSender> ();
            services.AddScoped<FileUploadService> ();
            services.AddMvc ();
            services.AddSignalR ();
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseBrowserLink ();
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
            }

            app.UseStaticFiles ();

            app.UseAuthentication ();

            app.UseSignalR (routes => {
                routes.MapHub<ChatHub> ("/chatHubs");
            });

            app.UseMvc (routes => {

                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute (
                    name: "default2",
                    template: "{controller=Offices}/{action=Index}/{id?}");
            });

            //new AppDBInitializer().SeedAsync(app).GetAwaiter();

        }
    }
}