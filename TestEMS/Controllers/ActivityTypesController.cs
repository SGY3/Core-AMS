using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestEMS.Data;
using TestEMS.Models;

namespace TestEMS.Controllers
{
    public class ActivityTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActivityTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActivityTypes.ToListAsync());
        }

        // GET: ActivityTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityTypes = await _context.ActivityTypes
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (activityTypes == null)
            {
                return NotFound();
            }

            return View(activityTypes);
        }

        // GET: ActivityTypes/Create
        public IActionResult Create()
        {
            ViewBag.IsActiveList = new SelectList(new[]
            {
                new { Value = "Y", Text = "Yes" },
                new { Value = "N", Text = "No" }
            }, "Value", "Text");
            return View();
        }

        // POST: ActivityTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,ActivityName,IsActive")] ActivityTypes activityTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activityTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activityTypes);
        }

        // GET: ActivityTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityTypes = await _context.ActivityTypes.FindAsync(id);
            if (activityTypes == null)
            {
                return NotFound();
            }
            ViewBag.IsActiveList = new SelectList(new[]
            {
                new { Value = "Y", Text = "Yes" },
                new { Value = "N", Text = "No" }
            }, "Value", "Text", activityTypes.IsActive); // Pre-select the current value

            return View(activityTypes);
        }

        // POST: ActivityTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityId,ActivityName,IsActive")] ActivityTypes activityTypes)
        {
            if (id != activityTypes.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activityTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityTypesExists(activityTypes.ActivityId))
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
            return View(activityTypes);
        }

        // GET: ActivityTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityTypes = await _context.ActivityTypes
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (activityTypes == null)
            {
                return NotFound();
            }

            return View(activityTypes);
        }

        // POST: ActivityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activityTypes = await _context.ActivityTypes.FindAsync(id);
            if (activityTypes != null)
            {
                _context.ActivityTypes.Remove(activityTypes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityTypesExists(int id)
        {
            return _context.ActivityTypes.Any(e => e.ActivityId == id);
        }
    }
}
