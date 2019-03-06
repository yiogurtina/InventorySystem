using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.ViewComponents
{
    public class EventsCount : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public EventsCount(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string Invoke(string userId)
        {
            var events = context.AssetEvents.Where(e => e.EmployeeId == userId);

            List<EventAsset> expiringEvents = new List<EventAsset>();
            foreach (var ev in events)
            {
                double diff = (ev.DeadLine - ev.CreationDate).TotalDays;
                if (diff < ev.BeforeAlertDays)
                {
                    expiringEvents.Add(ev);
                }
            }

            return expiringEvents.Count().ToString();
        }
    }
}
