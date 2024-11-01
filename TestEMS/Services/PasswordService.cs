using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Security.Policy;
using TestEMS.Models;

namespace TestEMS.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<EmployeeData> _passwordHasher;
        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<EmployeeData>();
        }
        //Method to hash the password
        public string HashPassword(EmployeeData employee, string password)
        {
            return _passwordHasher.HashPassword(employee, password);
        }

        // Method to verify the password
        public bool VerifyPassword(EmployeeData employee, string password, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(employee, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
