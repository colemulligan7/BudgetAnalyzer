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

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5137")
            });
        builder.Services.AddMudServices();
        builder.Services.AddHttpClient<IFileService, FileService>(client => {
            client.BaseAddress = new Uri("http://localhost:5137");
        });
        var app = builder.Build();



        await app.RunAsync();
    }
}