using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetAnalyzer.Shared.Models
{
    public class Allocation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Transaction? Transaction { get; set; }

        public Allocation? ParentAllocation { get; set; }

        public Category? Category { get; set; }

        public string? Nickname { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(19,4)")]
        public decimal Amount { get; set; }

    }
}
