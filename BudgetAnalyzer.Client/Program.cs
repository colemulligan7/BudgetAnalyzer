using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using BudgetAnalyzer.Client.Data;
using BudgetAnalyzer.Client.Data.Interfaces;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddHttpClient<IFileService, FileService>(client => {
            client.BaseAddress = new Uri("http://localhost:5137");
        });
        builder.Services.AddHttpClient<ITransactionService, TransactionService>(client => {
            client.BaseAddress = new Uri("http://localhost:5137");
        });
        builder.Services.AddMudServices();
        builder.Services.AddScoped<MudBlazor.DialogService>();


        await builder.Build().RunAsync();
    }
}