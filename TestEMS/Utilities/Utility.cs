using Microsoft.EntityFrameworkCore;
using TestEMS.Data;

namespace TestEMS.Utilities
{
    public class Utility
    {
        private readonly ApplicationDbContext _context;
        public Utility(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateToDoID()
        {
            var todoCount = await _context.ToDo.CountAsync();
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmm");
            var toDoId = $"T-{currentTime}-{todoCount + 1}";
            return toDoId;
        }
        public async Task<string> GenerateActivityID()
        {
            var todoCount = await _context.MyActivity.CountAsync();
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmm");
            var toDoId = $"A-{currentTime}-{todoCount + 1}";
            return toDoId;
        }
    }
}
