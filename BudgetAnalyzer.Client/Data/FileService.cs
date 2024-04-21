using BudgetAnalyzer.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BudgetAnalyzer.Client.Data
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UploadTransactions(IBrowserFile file, long transactionFileMappingId)
        {

            using var form = new MultipartFormDataContent();


            var fileContent =
                        new StreamContent(file.OpenReadStream(512000));

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);

            form.Add(
                content: fileContent,
                name: "\"files\"",
                fileName: file.Name);

            _httpClient.DefaultRequestHeaders.Add("TransactionFileMappingId", transactionFileMappingId.ToString());



            var test = await _httpClient.PostAsync("api/Files/UploadTransactions", form);
            Console.WriteLine(test);
        }

    }
}
