namespace BudgetAnalyzer.CsvMapping
{
    public class TransactionCsv
    {
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public int TransactionType { get; set; }
    }
}
