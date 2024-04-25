using BudgetAnalyzer.Client.Data.Interfaces;
using BudgetAnalyzer.Client.Data;
using BudgetAnalyzer.Components;
using BudgetAnalyzer.Data;
using BudgetAnalyzer.Services.Interfaces;
using BudgetAnalyzer.Services.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using BudgetAnalyzer.Shared.Models;
using System;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<BudgetAnalyzer.Services.Interfaces.ITransactionService, BudgetAnalyzer.Services.Logic.TransactionService>();

builder.Services.AddHttpClient<IFileService, FileService>(client => {
    client.BaseAddress = new Uri("http://localhost:5137");
});

builder.Services.AddHttpClient<BudgetAnalyzer.Client.Data.Interfaces.ITransactionService, BudgetAnalyzer.Client.Data.TransactionService>(client => {
    client.BaseAddress = new Uri("http://localhost:5137");
});

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddMudServices()
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<MudBlazor.DialogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapSwagger();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BudgetAnalyzer.Client._Imports).Assembly);

await app.RunAsync();
