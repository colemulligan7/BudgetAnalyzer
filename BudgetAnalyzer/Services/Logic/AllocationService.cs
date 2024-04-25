using BudgetAnalyzer.Data;
using BudgetAnalyzer.Shared.Models;

namespace BudgetAnalyzer.Services.Logic
{
    public class AllocationService
    {
        private readonly ApplicationDbContext _context;
        public AllocationService(ApplicationDbContext context)
        {
            _context = context;

        }

        public IEnumerable<Allocation> GetAllocations()
        {
            return _context.Allocations.AsEnumerable();
        }

    }
}
