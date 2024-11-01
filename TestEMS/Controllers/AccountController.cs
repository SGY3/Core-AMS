using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestEMS.Data;
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
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {

                var employee = await _context.EmployeeData.FirstOrDefaultAsync(e => e.UserName == username);
                if (employee != null)
                {
                    PasswordService passwordService = new PasswordService();
                    bool isPasswordValid = passwordService.VerifyPassword(employee, password, employee.PasswordHash);
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
    }
}
