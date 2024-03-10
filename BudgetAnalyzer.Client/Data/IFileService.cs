using Microsoft.AspNetCore.Components.Forms;

namespace BudgetAnalyzer.Client.Data
{
    public interface IFileService
    {
        Task UploadTransactions(IBrowserFile file);
    }
}