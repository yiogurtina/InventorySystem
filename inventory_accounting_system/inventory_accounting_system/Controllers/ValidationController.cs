using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using Microsoft.AspNetCore.Mvc;

namespace inventory_accounting_system.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ValidationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AcceptVerbs("Get")]
        public IActionResult ValidateInventNumber(string inventNumber, string id)
        {

            if (_context.Assets.Any(c => c.InventNumber == inventNumber && c.Id != id))
                return Json(false);

            return Json(true);
        }

    }
}