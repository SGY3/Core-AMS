using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestEMS.Data;
using TestEMS.Models;

namespace TestEMS.Controllers
{
    public class ToDoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToDoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToDoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToDo.Include(t => t.ActivityType).Include(t => t.Project).Where(t => t.AssignedTo == null);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToDoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityType)
                .Include(t => t.Project)
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
        public async Task<IActionResult> Create([Bind("Title,Description,ProjectId,ActivityTypeId,PageName,Priority")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                toDo.CreatedBy = HttpContext.Session.GetString("Id");
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
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("ToDoId,Title,Description,ProjectId,ActivityTypeId,PageName,Priority")] ToDo toDo)
        {
            if (id != toDo.ToDoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    toDo.CreatedBy = "Admin";
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
        public async Task<IActionResult> Assign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityType)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.ToDoId == id);
            if (toDo == null)
            {
                return NotFound();
            }
            ViewBag.EmployeeList = _context.EmployeeData.ToList();
            return View(toDo);
        }
        // POST: ToDoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Assign")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignConfirmed(int id, [Bind("ToDoId,AssignedTo")] ToDo toDo)
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

            _context.ToDo.Update(toDoData);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: ToDoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDo = await _context.ToDo
                .Include(t => t.ActivityType)
                .Include(t => t.Project)
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

        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.ToDoId == id);
        }
    }
}
