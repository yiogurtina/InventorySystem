using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace inventory_accounting_system.Controllers
{
    public class AssetEventController : Controller
    {
        private readonly ApplicationDbContext context;

        public AssetEventController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult DeleteEvent(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var _event = context.AssetEvents.Find(id);
            context.Remove(_event);
            context.SaveChanges();
            return RedirectToAction("Index", "Assets");
        }

        public IActionResult AddEvent(string assetId)
        {
            List<string> periods = new List<string>()
            {
                "Ежедневно",
                "Еженедельно",
                "Ежемесячно",
                "Ежегодно"
            };
            ViewData["Periods"] = new SelectList(periods);
            ViewData["AssetId"] = assetId;
            return View();
        }

        public IActionResult AddNewEvent(EventAsset _event, string periodicity, string assetId)
        {
            if (assetId == null)
            {
                return NotFound();
            }

            var asset = context.Assets.Find(assetId);

            int period;
            switch (periodicity)
            {
                case "Ежедневно":
                    period = 1;
                    break;
                case "Еженедельно":
                    period = 7;
                    break;
                case "Ежемесячно":
                    period = 31;
                    break;
                case "Ежегодно":
                    period = 365;
                    break;
                default:
                    period = 0;
                    break;
            }
            var assetEvent = new EventAsset()
            {
                Title = _event.Title,
                Content = _event.Content,
                Period = period,
                CreationDate = DateTime.Now,
                DeadLine = DateTime.Now.AddDays(period),
                AssetId = assetId,
                EmployeeId = asset.EmployeeId
            };
            context.Add(assetEvent);
            context.SaveChanges();

            return RedirectToAction("Index", "Assets");
        }
    }
}