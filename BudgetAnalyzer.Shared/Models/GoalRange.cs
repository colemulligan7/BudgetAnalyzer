using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetAnalyzer.Shared.Models
{
    public class GoalRange
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public required Category Category { get; set; }

        [Column(TypeName = "decimal(19,4)")]
        public decimal Amount { get; set; }

    }
}
