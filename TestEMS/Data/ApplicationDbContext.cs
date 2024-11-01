using Microsoft.EntityFrameworkCore;
using TestEMS.Models;

namespace TestEMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ActivityTypes> ActivityTypes { get; set; } = default!;
        public DbSet<Project> Project { get; set; } = default!;
        public DbSet<EmployeeData> EmployeeData { get; set; } = default!;
        public DbSet<ToDo> ToDo { get; set; } = default!;
        public DbSet<TestEMS.Models.MyActivity> MyActivity { get; set; } = default!;
    }
}
