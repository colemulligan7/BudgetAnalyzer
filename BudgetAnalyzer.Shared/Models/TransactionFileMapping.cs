using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetAnalyzer.Shared.Models
{
    public class TransactionFileMapping
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string TransactionDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string Description { get; set; }

        public string? AmountPaid { get; set; }

        public string? AmountReceived { get; set;}

    }
}
