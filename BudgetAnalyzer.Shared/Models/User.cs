using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAnalyzer.Shared.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? Settings { get; set; }

    }
}
