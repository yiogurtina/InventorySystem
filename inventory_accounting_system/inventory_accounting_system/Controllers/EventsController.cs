using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public EventsController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var events = _context.Events.Include(a => a.Category);
            return View(await events.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _context.Events
                .Include(c => c.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (_event == null)
            {
                return NotFound();
            }

            return View(_event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            List<string> periods = new List<string>()
            {
                "Ежедневно",
                "Еженедельно",
                "Ежемесячно",
                "Ежегодно"
            };
            ViewData["Periods"] = new SelectList(periods);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,CategoryId,Content,Id,Periodicity")] Event _event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", _event.CategoryId);
            return View(_event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            if (_event == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", _event.CategoryId);
            return View(_event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Title,CreationDate,CategoryId,DaysCountBeforeAlert,AssetId,Id")] Event _event)
        {
            if (id != _event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_event);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(_event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", _event.CategoryId);
            return View(_event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _event = await _context.Events
                .Include(c => c.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (_event == null)
            {
                return NotFound();
            }

            return View(_event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var _event = await _context.Events.FindAsync(id);
            if (_event == null)
            {
                return NotFound();
            }
            _context.Events.Remove(_event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(string id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        public void UpdateExpiredEvents()
        {
            var currUserId = _userManager.GetUserId(User);

            var events = _context.AssetEvents.Where(a => a.EmployeeId == currUserId);

            if (events.Count() != 0)
            {
                foreach (var ev in events)
                {
                    if (ev.DeadLine > ev.CreationDate)
                    {
                        ev.DeadLine = DateTime.Now.AddDays(ev.Period);
                        _context.Update(ev);
                    }
                }

                _context.SaveChanges();
            }
        }
    }
}
