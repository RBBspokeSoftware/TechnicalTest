using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

internal partial class Program
{
    private static int UserId { get; set; }
    private static WebApplication App { get; set; }
    
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationContext>();
        
        App = builder.Build();

        if (App.Environment.IsDevelopment())
        {
            App.UseSwagger();
            App.UseSwaggerUI();
        }

        using (var scope = App.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            await db.Database.MigrateAsync();
        }

        App.UseHttpsRedirection();

        App.UseAuthorization();

        App.MapControllers();

        UserId = 0;

        App.MapGet("/", () => "Hello world");

        SetGetCustomerMapping();
        SetAddCustomerMapping();
        SetEditCustomerMapping();
        SetDeleteCustomerMapping();
        SetGetBankAccountMapping();
        SetAddBankAccountMapping();
        SetEditBankAccountMapping();
        SetDeleteBankAccountMapping();

        App.Run();
    }
}