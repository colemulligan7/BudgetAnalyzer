using BudgetAnalyzer.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace BudgetAnalyzer.Client.Data.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionFileMapping>> GetTransactionTemplates();

        Task CreateTransactionMapping(TransactionFileMapping mapping);


    }
}