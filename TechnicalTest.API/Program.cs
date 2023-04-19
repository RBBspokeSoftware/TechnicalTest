using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello world");

int userID = 0;

app.MapPost("/customers", async ([FromBody] AddCustomerModel customer, ApplicationContext db) =>
{
    db.Customers.Add((Customer)DateAndUserInfoUpdate.UpdateCreateInfo(new Customer
    {
        Name = customer.Name,
        DateOfBirth = customer.DateOfBirth,
        DailyTransferLimit = customer.DailyTransferLimit,
    }, userID));

    await db.SaveChangesAsync();
     
    return Results.Ok();
});

app.MapGet("/customers", (ApplicationContext db) =>
{
    var customers = db.Customers
      .Where(x => x.DeletedByUserID == null).ToList();

    return Results.Ok(customers);
});

app.MapGet("/customers-all", (ApplicationContext db) =>
{
    var customers = db.Customers
        .Select(x => new
        {
            x.Id,
            x.Name,
            x.DateOfBirth,
            x.BankAccounts,
            x.DailyTransferLimit,
            x.CreateDate,
            x.CreatedByUserID,
            x.UpdateDate,
            x.UpdatedByUserID,
            x.DeleteDate,
            x.DeletedByUserID
        }).ToList();

    return Results.Ok(customers);
});

app.MapGet("/customers-deleted", (ApplicationContext db) =>
{
    var customers = db.Customers
        .Where(x => x.DeletedByUserID != null).ToList();

    return Results.Ok(customers);
});


app.MapPost("/customers-edit", async ([FromBody] Customer customer, ApplicationContext db) =>
{
    db.Customers.Update((Customer)DateAndUserInfoUpdate.UpdateUpdateInfo(customer, userID));

    await db.SaveChangesAsync();

    return Results.Ok();
});



app.MapPost("/customers-delete", async ([FromBody] Customer customer, ApplicationContext db) =>
{
    db.Customers.Update((Customer)DateAndUserInfoUpdate.UpdateDeleteInfo(customer, userID));

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();