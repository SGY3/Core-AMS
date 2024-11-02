using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestEMS.Data;
using TestEMS.Models;
using TestEMS.Services;

namespace TestEMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.EmployeeData.FirstOrDefaultAsync(e => e.UserName == Username);
                if (employee != null)
                {
                    PasswordService passwordService = new PasswordService();
                    bool isPasswordValid = passwordService.VerifyPassword(employee, Password, employee.PasswordHash);
                    if (isPasswordValid)
                    {
                        // Login successful, store employee details in session
                        HttpContext.Session.SetString("EmployeeId", employee.EmployeeId);
                        HttpContext.Session.SetString("Name", employee.Name);
                        HttpContext.Session.SetString("Id", employee.Id.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(Register register)
        {
            if (ModelState.IsValid)
            {
                var employeeData = await _context.EmployeeData.FirstOrDefaultAsync(i => i.EmployeeId == register.EmployeeId);
                if (employeeData != null)
                {
                    ModelState.AddModelError(string.Empty, "Employee id already exists.");
                }
                EmployeeData employee = new EmployeeData
                {
                    Name = register.Name,
                    EmployeeId = register.EmployeeId,
                    Email = register.Email,
                    Phone = register.Phone,
                    UserName = register.UserName,
                    IsActive = "Y"
                };

                PasswordService passwordService = new PasswordService();
                employee.PasswordHash = passwordService.HashPassword(employee, register.Password);
                _context.EmployeeData.Add(employee);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User created successfully";

                return RedirectToAction(nameof(Login));
            }
            return View(register);
        }
    }
}
