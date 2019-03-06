using inventory_accounting_system.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.ViewComponents
{
    public class EventsViewComponent : ViewComponent
    {
        private readonly UserManager<Employee> userManager;
        private readonly ApplicationDbContext context;

        public EventsViewComponent(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke(string userId)
        {
            var events = context.AssetEvents.Where(a => a.EmployeeId == userId);

            List<EventAsset> expiringEvents = new List<EventAsset>();
            foreach (var ev in events)
            {
                double diff = (ev.DeadLine - ev.CreationDate).TotalDays;
                if (diff < ev.BeforeAlertDays)
                {
                    expiringEvents.Add(ev);
                }
            }

            return View(expiringEvents);

        }
    }
}
