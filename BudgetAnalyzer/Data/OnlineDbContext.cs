using BudgetAnalyzer.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BudgetAnalyzer.Data
{
    public class OnlineDbContext : DbContext
    {
        public OnlineDbContext(
            DbContextOptions<OnlineDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Allocation> Allocations { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<GoalRange> Goals { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

    }
}
