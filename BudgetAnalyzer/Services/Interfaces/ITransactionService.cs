using BudgetAnalyzer.CsvMapping;
using BudgetAnalyzer.Shared.Models;
namespace BudgetAnalyzer.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<string> ProcessTransactions(IEnumerable<Transaction> transactions);

        TransactionFileMapping? GetTransactionFileMappingTemplate(long transactionFileMappingId);

        IEnumerable<TransactionFileMapping> GetTransactionFileMappingsTemplates();

        string CreateTransactionMapping(TransactionFileMapping mapping);

    }
}