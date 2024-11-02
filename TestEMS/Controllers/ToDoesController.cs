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
    public class ToDoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Utility _utility;

        public ToDoesController(ApplicationDbContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        // GET: ToDoes
        public async Task<IActionResult> Index()
        {
            //var todos = await _context.ToDo.Select(t => new
            //{
            //    t.ToDoId,
            //    t.Title,
            //    t.Description,
            //    ProjectName = t.Project.ProjectName,
            //    ActivityName = t.ActivityType.ActivityName,
            //    t.PageName,
            //    CreatedByName = t.AddedByEmployeeData.Name,
            //    AssignToName = t.AssignedByEmployeeData.Name
            //}).ToListAsync();
            //return View(todos);
            var applicationDbContext = _context.ToDo
                .Include(t => t.ActivityType)
                .Include(t => t.Project)
                .Include(t => t.AddedByEmployeeData)
                .Include(t => t.AssignedByEmployeeData)
                .Where(t => t.AssignedTo == null);
            return View(await applicationDbContext.ToListAsync());

        }

        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityType)
                .Include(t => t.Project)
                .Include(t => t.AddedByEmployeeData)
                .Include(t => t.AssignedByEmployeeData)
                .Where(t => t.AssignedTo == null)
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDo == null)
            {
                return NotFound();
            }
            return View(toDo);
        }

        // GET: ToDoes/Create
        public IActionResult Create()
        {
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityId", "ActivityName");
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName");

            ViewBag.PriorityList = new SelectList(new[]
            {
                new { Value = "Low", Text = "Low" },
                new { Value = "Medium", Text = "Medium" },
                new { Value = "High", Text = "High" }
            }, "Value", "Text");


            return View();
        }

        // POST: ToDoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoId,Title,Description,ProjectId,ActivityTypeId,PageName,Priority")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                toDo.ToDoId = await _utility.GenerateToDoID();
                toDo.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                _context.Add(toDo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityId", "ActivityName", toDo.ActivityTypeId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName", toDo.ProjectId);
            ViewBag.PriorityList = new SelectList(new[]
            {
                new { Value = "Low", Text = "Low" },
                new { Value = "Medium", Text = "Medium" },
                new { Value = "High", Text = "High" }
            }, "Value", "Text");
            return View(toDo);
        }

        // GET: ToDoes/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityId", "ActivityName", toDo.ActivityTypeId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "IsActive", toDo.ProjectId);
            ViewBag.PriorityList = new SelectList(new[]
            {
                new { Value = "Low", Text = "Low" },
                new { Value = "Medium", Text = "Medium" },
                new { Value = "High", Text = "High" }
            }, "Value", "Text", toDo.Priority);
            return View(toDo);
        }

        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ToDoId,Title,Description,ProjectId,ActivityTypeId,PageName,Priority")] ToDo toDo)
        {
            if (id != toDo.ToDoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    toDo.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    _context.Update(toDo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoExists(toDo.ToDoId))
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
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "ActivityId", "ActivityName", toDo.ActivityTypeId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "IsActive", toDo.ProjectId);
            return View(toDo);
        }

        // GET: ToDoes/Assing/5
        public async Task<IActionResult> Assign(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityType)
                .Include(t => t.Project)
                .Include(t => t.AddedByEmployeeData)
                .Include(t => t.AssignedByEmployeeData)
                .Where(t => t.AssignedTo == null)
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDo == null)
            {
                return NotFound();
            }
            ViewBag.EmployeeList = _context.EmployeeData.ToList();
            return View(toDo);
        }
        // POST: ToDoes/Assign/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Assign")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignConfirmed(string id, [Bind("ToDoId,AssignedTo")] ToDo toDo)
        {
            if (id != toDo.ToDoId)
            {
                return NotFound();
            }

            var toDoData = await _context.ToDo.FindAsync(id);
            if (toDoData == null)
            {
                return NotFound();
            }

            // Assign the ToDo to the selected employee
            toDoData.AssignedTo = toDo.AssignedTo;

            MyActivity myActivity = new MyActivity();

            myActivity.ActivityId = await _utility.GenerateActivityID();
            myActivity.ToDoId = toDoData.ToDoId;
            myActivity.ActivityTitle = toDoData.Title;
            myActivity.ActivityDescription = toDoData.Description;
            myActivity.ProjectId = toDoData.ProjectId;
            myActivity.ActivityTypeId = toDoData.ActivityTypeId;
            myActivity.PageName = toDoData.PageName;
            myActivity.Priority = toDoData.Priority;
            myActivity.AssignedTo = toDoData.AssignedTo;
            myActivity.AssignedBy = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            myActivity.AssignedDate = DateTime.Now;

            _context.MyActivity.Add(myActivity);
            _context.ToDo.Update(toDoData);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: ToDoes/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityType)
                .Include(t => t.Project)
                .Include(t => t.AddedByEmployeeData)
                .Include(t => t.AssignedByEmployeeData)
                .Where(t => t.AssignedTo == null)
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDo = await _context.ToDo.FindAsync(id);
            if (toDo != null)
            {
                _context.ToDo.Remove(toDo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoExists(string id)
        {
            return _context.ToDo.Any(e => e.ToDoId == id);
        }
    }
}
