using BudgetAnalyzer.Client.Data.Interfaces;
using BudgetAnalyzer.Shared.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BudgetAnalyzer.Client.Data
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TransactionFileMapping>> GetTransactionTemplates()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("api/Transaction/GetTransactionTemplates");
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<TransactionFileMapping>>();
            return content!;

        }

        public async Task CreateTransactionMapping(TransactionFileMapping mapping)
        {
            using StringContent jsonContent = new(JsonSerializer.Serialize(mapping),
                Encoding.UTF8,
                "application/json");

            var response = await
            _httpClient.PostAsync("api/Transaction/CreateTransactionMapping", jsonContent);

            if(response.IsSuccessStatusCode)
            {

            }else
            {

            }

        }


    }
}
