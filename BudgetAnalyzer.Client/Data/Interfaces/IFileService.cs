using Microsoft.AspNetCore.Components.Forms;

namespace BudgetAnalyzer.Client.Data.Interfaces
{
    public interface IFileService
    {
        Task UploadTransactions(IBrowserFile file, long transactionFileMappingId);
    }
}