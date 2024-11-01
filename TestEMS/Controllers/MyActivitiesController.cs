using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestEMS.Data;
using TestEMS.Filters;
using TestEMS.Models;
using TestEMS.Utilities;

namespace TestEMS.Controllers
{
    [SessionRequired]
    public class MyActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Utility _utility;

        public MyActivitiesController(ApplicationDbContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        // GET: MyActivities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MyActivity.Include(m => m.ActivityType)
                .Include(m => m.AssignedByEmployeeData).Include(m => m.AssignedToEmployeeData)
                .Include(m => m.Project).Where(m => m.AssignedTo == Convert.ToInt32(HttpContext.Session.GetString("Id")));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MyActivities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivity
                .Include(m => m.ActivityType)
                .Include(m => m.AssignedByEmployeeData)
                .Include(m => m.AssignedToEmployeeData)
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (myActivity == null)
            {
                return NotFound();
            }

            return View(myActivity);
        }

        // GET: MyActivities/Create
        public IActionResult Create()
        {
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityId", "ActivityName");
            ViewData["AssignedBy"] = new SelectList(_context.EmployeeData, "Id", "Name");
            ViewData["AssignedTo"] = new SelectList(_context.EmployeeData, "Id", "Name");
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName");
            return View();
        }

        // POST: MyActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,ToDoId,ActitivityTitle,ActivityDescription,ProjectId,ActivityTypeId,PageName,Priority,AssignedTo,AssignedBy,AssignedDate")] MyActivity myActivity)
        {
            if (ModelState.IsValid)
            {
                myActivity.ActivityId = await _utility.GenerateActivityID();
                _context.Add(myActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityId", "ActivityName", myActivity.ActivityTypeId);
            ViewData["AssignedBy"] = new SelectList(_context.EmployeeData, "Id", "EmployeeId", myActivity.AssignedBy);
            ViewData["AssignedTo"] = new SelectList(_context.EmployeeData, "Id", "EmployeeId", myActivity.AssignedTo);
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "IsActive", myActivity.ProjectId);
            return View(myActivity);
        }

        // GET: MyActivities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivity.FindAsync(id);
            if (myActivity == null)
            {
                return NotFound();
            }
            MyActivityViewModel myActivityViewModel = new MyActivityViewModel();
            myActivityViewModel.ActivityId = myActivity.ActivityId;
            myActivityViewModel.DisplayActivityId= myActivity.ActivityId;
            myActivityViewModel.ActivityTitle= myActivity.ActivityTitle;
            myActivityViewModel.ActivityDescription= myActivity.ActivityDescription;
            return View(myActivityViewModel);
        }

        // POST: MyActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ActivityId,DisplayActivityId,ActivityTitle,ActivityDescription")] MyActivityViewModel myActivity)
        {
            if (id != myActivity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var activity = await _context.MyActivity.FindAsync(id);
                    if (activity == null)
                    {
                        return NotFound();
                    }
                    activity.ActivityTitle = myActivity.ActivityTitle;   
                    activity.ActivityDescription = myActivity.ActivityDescription;   

                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyActivityExists(myActivity.ActivityId))
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
            return View(myActivity);
        }

        // GET: MyActivities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myActivity = await _context.MyActivity
                .Include(m => m.ActivityType)
                .Include(m => m.AssignedByEmployeeData)
                .Include(m => m.AssignedToEmployeeData)
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (myActivity == null)
            {
                return NotFound();
            }

            return View(myActivity);
        }

        // POST: MyActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var myActivity = await _context.MyActivity.FindAsync(id);
            if (myActivity != null)
            {
                _context.MyActivity.Remove(myActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyActivityExists(string id)
        {
            return _context.MyActivity.Any(e => e.ActivityId == id);
        }
    }
}
