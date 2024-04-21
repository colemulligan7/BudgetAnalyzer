using BudgetAnalyzer.CsvMapping;
using BudgetAnalyzer.Data;
using BudgetAnalyzer.Services.Interfaces;
using BudgetAnalyzer.Shared.Models;

namespace BudgetAnalyzer.Services.Logic
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        public TransactionService(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<string> ProcessTransactions(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                var matchingTransaction = _context.Transactions.Where(x => x.Amount == transaction.Amount
                                                && x.Description == transaction.Description
                                                && x.DateOfTransaction == transaction.DateOfTransaction).FirstOrDefault();


                if (matchingTransaction == null)
                {
                    _context.Update(transaction);
                    var categorySearch = _context.CategorySearch.Where(x => transaction.Description.Contains(x.SearchTerm));


                    var newAllocation = new Allocation()
                    {
                        Description = transaction.Description,
                        Amount = transaction.Amount,
                        Transaction = transaction

                    };

                    if (categorySearch == null)
                    {

                    }
                    else if (categorySearch.Count() > 1)
                    {

                    }
                    else if (categorySearch.Any())
                    {
                        newAllocation.Category = categorySearch.FirstOrDefault()?.Category;
                        
                    }
                    _context.Allocations.Add(newAllocation);
                }
            }

            return "";

        }

        public IEnumerable<TransactionFileMapping> GetTransactionFileMappingsTemplates()
        {
            return _context.TransactionFileMappings.AsEnumerable();
        }

        public TransactionFileMapping? GetTransactionFileMappingTemplate(long transactionFileMappingId)
        {

            return _context.TransactionFileMappings.FirstOrDefault(x => x.Id == transactionFileMappingId);

        }

        public string CreateTransactionMapping(TransactionFileMapping mapping)
        {
            _context.TransactionFileMappings.Add(mapping);
            _context.SaveChanges();
            return "True";
        }

        public static int Calculate(string source1, string source2) //O(n*m)
        {
            var source1Length = source1.Length;
            var source2Length = source2.Length;

            var matrix = new int[source1Length + 1, source2Length + 1];

            // First calculation, if one entry is empty return full length
            if (source1Length == 0)
                return source2Length;

            if (source2Length == 0)
                return source1Length;

            // Initialization of matrix with row size source1Length and columns size source2Length
            for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
            for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

            // Calculate rows and collumns distances
            for (var i = 1; i <= source1Length; i++)
            {
                for (var j = 1; j <= source2Length; j++)
                {
                    var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }
            // return result
            return matrix[source1Length, source2Length];
        }


    }
}