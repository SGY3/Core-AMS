using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestEMS.Data;
using TestEMS.Models;
using TestEMS.Services;

namespace TestEMS.Controllers
{
    public class EmployeeDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeDatas
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeData.ToListAsync());
        }

        // GET: EmployeeDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeeData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeData == null)
            {
                return NotFound();
            }

            return View(employeeData);
        }

        // GET: EmployeeDatas/Create
        public IActionResult Create()
        {
            ViewBag.IsActiveList = new SelectList(new[]
            {
                new { Value = "Y", Text = "Yes" },
                new { Value = "N", Text = "No" }
            }, "Value", "Text");
            return View();
        }

        // POST: EmployeeDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Name,Email,Phone,UserName,PasswordHash,IsActive")] EmployeeData employeeData)
        {
            if (ModelState.IsValid)
            {
                PasswordService passwordService = new PasswordService();
                employeeData.PasswordHash = passwordService.HashPassword(employeeData, employeeData.PasswordHash);
                _context.Add(employeeData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeData);
        }

        // GET: EmployeeDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeeData.FindAsync(id);
            if (employeeData == null)
            {
                return NotFound();
            }
            var empViewModel = new EmployeeViewModel
            {
                Id = employeeData.Id,
                EmployeeId = employeeData.EmployeeId,
                Name = employeeData.Name,
                Phone = employeeData.Phone,
                Email = employeeData.Email,
                UserName = employeeData.UserName
            };
            ViewBag.IsActiveList = new SelectList(new[]
            {
                new { Value = "Y", Text = "Yes" },
                new { Value = "N", Text = "No" }
            }, "Value", "Text", employeeData.IsActive);
            return View(empViewModel);
        }

        // POST: EmployeeDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Name,Email,Phone,UserName,IsActive")] EmployeeViewModel employeeData)
        {
            if (id != employeeData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = await _context.EmployeeData.FindAsync(employeeData.Id);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }
                    existingEmployee.EmployeeId = employeeData.EmployeeId;
                    existingEmployee.Name = employeeData.Name;
                    existingEmployee.Phone = employeeData.Phone;
                    existingEmployee.Email = employeeData.Email;
                    existingEmployee.UserName = employeeData.UserName;
                    existingEmployee.IsActive = employeeData.IsActive;
                    _context.Update(existingEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDataExists(employeeData.Id))
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

            ViewBag.IsActiveList = new SelectList(new[]
            {
                new { Value = "Y", Text = "Yes" },
                new { Value = "N", Text = "No" }
            }, "Value", "Text", employeeData.IsActive);
            return View(employeeData);
        }

        // GET: EmployeeDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeData = await _context.EmployeeData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeData == null)
            {
                return NotFound();
            }

            return View(employeeData);
        }

        // POST: EmployeeDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeData = await _context.EmployeeData.FindAsync(id);
            if (employeeData != null)
            {
                _context.EmployeeData.Remove(employeeData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDataExists(int id)
        {
            return _context.EmployeeData.Any(e => e.Id == id);
        }
    }
}
