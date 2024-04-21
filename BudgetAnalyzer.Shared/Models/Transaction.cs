using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BudgetAnalyzer.Shared.Models
{
    public class Transaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "decimal(19,4)")]
        public decimal Amount { get; set; }

        public required string Description { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public int TransactionType { get; set; }

        public bool MatchingTransactions { get; set; }
    }
}
