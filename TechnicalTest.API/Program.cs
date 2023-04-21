using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
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

int userId = 0;

app.MapGet("/", () => "Hello world");


void MapDataModels()
{
    // void AddCustomerMapping()
    // {
    //     void GetMapping()
    //     {
    //         //Customer Get 
    //         app.MapGet("/customers", (ApplicationContext db) =>
    //         {
    //             var customers = db.Customers
    //                 .Where(x => x.DeletedByUserID == null).Select(x => new
    //                 {
    //                     x.Id,
    //                     x.FirstName,
    //                     x.MiddleNames,
    //                     x.LastName,
    //                     x.DateOfBirth,
    //                     x.BankAccounts,
    //                     x.DailyTransferLimit,
    //                     x.CreateDate,
    //                     x.CreatedByUserID,
    //                     x.UpdateDate,
    //                     x.UpdatedByUserID,
    //                     x.DeleteDate,
    //                     x.DeletedByUserID
    //                 }).ToList();
    //
    //             return Results.Ok(customers);
    //         });
    //
    //         app.MapGet("/customers/{id}", async (int id, ApplicationContext db) =>
    //         {
    //             var dbCustomer = db.Customers.FirstOrDefault(x => x.Id == id);
    //
    //             if (dbCustomer != null)
    //             {
    //                 return Results.Ok(new
    //                 {
    //                     dbCustomer.Id,
    //                     dbCustomer.FirstName,
    //                     dbCustomer.MiddleNames,
    //                     dbCustomer.LastName,
    //                     dbCustomer.DateOfBirth,
    //                     dbCustomer.BankAccounts,
    //                     dbCustomer.DailyTransferLimit,
    //                     dbCustomer.CreateDate,
    //                     dbCustomer.CreatedByUserID,
    //                     dbCustomer.UpdateDate,
    //                     dbCustomer.UpdatedByUserID,
    //                     dbCustomer.DeleteDate,
    //                     dbCustomer.DeletedByUserID
    //                 });
    //             }
    //
    //             return Results.NotFound();
    //         });
    //
    //         app.MapGet("/customers-all", (ApplicationContext db) =>
    //         {
    //             var customers = db.Customers
    //                 .Select(x => new
    //                 {
    //                     x.Id,
    //                     x.FirstName,
    //                     x.MiddleNames,
    //                     x.LastName,
    //                     x.DateOfBirth,
    //                     x.BankAccounts,
    //                     x.DailyTransferLimit,
    //                     x.CreateDate,
    //                     x.CreatedByUserID,
    //                     x.UpdateDate,
    //                     x.UpdatedByUserID,
    //                     x.DeleteDate,
    //                     x.DeletedByUserID
    //                 }).ToList();
    //
    //             return Results.Ok(customers);
    //         });
    //
    //         app.MapGet("/customers-deleted", (ApplicationContext db) =>
    //         {
    //             var customers = db.Customers
    //                 .Where(x => x.DeletedByUserID != null).Select(x => new
    //                 {
    //                     x.Id,
    //                     x.FirstName,
    //                     x.MiddleNames,
    //                     x.LastName,
    //                     x.DateOfBirth,
    //                     x.BankAccounts,
    //                     x.DailyTransferLimit,
    //                     x.CreateDate,
    //                     x.CreatedByUserID,
    //                     x.UpdateDate,
    //                     x.UpdatedByUserID,
    //                     x.DeleteDate,
    //                     x.DeletedByUserID
    //                 }).ToList();
    //
    //             return Results.Ok(customers);
    //         });
    //
    //         app.MapPost("/customers", async ([FromBody] AddCustomerModel customer, ApplicationContext db) =>
    //         {
    //             db.Customers.Add((Customer)DateAndUserInfoUpdate.UpdateCreateInfo(new Customer
    //             {
    //                 FirstName = customer.FirstName,
    //                 MiddleNames = customer.MiddleNames,
    //                 LastName = customer.LastName,
    //                 DateOfBirth = customer.DateOfBirth,
    //                 DailyTransferLimit = customer.DailyTransferLimit,
    //             }, userId));
    //
    //             await db.SaveChangesAsync();
    //
    //             return Results.Ok();
    //         });
    //     }
    //
    //     void EditMapping()
    //     {
    //         app.MapPost("/customers-edit", async ([FromBody] EditCustomerModel customer, ApplicationContext db) =>
    //         {
    //             var dbCustomer = db.Customers.FirstOrDefault(x => x.DeletedByUserID == null && x.Id == customer.id);
    //
    //             if (dbCustomer != null)
    //             {
    //                 dbCustomer.FirstName = customer.FirstName;
    //                 dbCustomer.MiddleNames = customer.MiddleNames;
    //                 dbCustomer.LastName = customer.LastName;
    //                 dbCustomer.DateOfBirth = customer.DateOfBirth;
    //                 dbCustomer.DailyTransferLimit = customer.DailyTransferLimit;
    //
    //                 db.Customers.Update((Customer)DateAndUserInfoUpdate.UpdateUpdateInfo(dbCustomer, userId));
    //
    //                 await db.SaveChangesAsync();
    //
    //                 return Results.Ok();
    //             }
    //
    //             return Results.NotFound();
    //         });
    //     }
    //
    //     void DeleteMapping()
    //     {
    //         app.MapPost("/customers-delete", async ([FromBody] int id, ApplicationContext db) =>
    //         {
    //             var customer = db.Customers.FirstOrDefault(x => x.Id == id);
    //             if (customer == null || customer.DeletedByUserID != null)
    //                 return Results.Ok();
    //
    //             db.Customers.Update((Customer)DateAndUserInfoUpdate.UpdateDeleteInfo(customer, userId));
    //             await db.SaveChangesAsync();
    //
    //             return Results.Ok();
    //         });
    //     }
    //
    //     GetMapping();
    //     EditMapping();
    //     DeleteMapping();
    // }

    void AddCustomerMapping()
    {
        void GetMapping()
        {
            object CoreCustomerData(Customer customer) => new
            {
                customer.Id,
                customer.FirstName,
                customer.MiddleNames,
                customer.LastName,
                customer.DateOfBirth,
                customer.DailyTransferLimit,
                customer.CreateDate,
                customer.CreatedByUserID,
                customer.UpdateDate,
                customer.UpdatedByUserID,
                customer.DeleteDate,
                customer.DeletedByUserID
            };

            //Customer Get 
            app.MapGet("/customer", (ApplicationContext db) =>
            {
                var customers = db.Customers.Where(x => x.DeletedByUserID == null).Select(CoreCustomerData).ToList();
                return Results.Ok(customers);
            });

            app.MapGet("/customer/{id:int}", (int id, ApplicationContext db) =>
            {
                var dbCustomer = db.Customers.FirstOrDefault(x => x.Id == id);

                return dbCustomer != null ? Results.Ok(CoreCustomerData(dbCustomer)) : Results.NotFound();
            });

            app.MapGet("/customer-all", (ApplicationContext db) =>
            {
                var customers = db.Customers.Select(CoreCustomerData).ToList();
                return Results.Ok(customers);
            });

            app.MapGet("/customer-deleted", (ApplicationContext db) =>
            {
                var customers = db.Customers.Where(x => x.DeletedByUserID != null).Select(CoreCustomerData).ToList();
                return Results.Ok(customers);
            });
        }

        void AddMapping()
        {
            app.MapPost("/customer", async ([FromBody] AddCustomerModel customer, ApplicationContext db) =>
            {
                db.Customers.Add((Customer)DateAndUserInfoUpdate.UpdateCreateInfo(new Customer
                {
                    FirstName = customer.FirstName,
                    MiddleNames = customer.MiddleNames,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth,
                    DailyTransferLimit = customer.DailyTransferLimit,
                }, userId));

                await db.SaveChangesAsync();

                return Results.Ok();
            });
        }

        void EditMapping()
        {
            app.MapPost("/customer-edit", async ([FromBody] EditCustomerModel customer, ApplicationContext db) =>
            {
                var dbCustomer = db.Customers.FirstOrDefault(x => x.DeletedByUserID == null && x.Id == customer.Id);

                if (dbCustomer == null)
                    return Results.NotFound();
                
                dbCustomer.FirstName = customer.FirstName;
                dbCustomer.MiddleNames = customer.MiddleNames;
                dbCustomer.LastName = customer.LastName;
                dbCustomer.DateOfBirth = customer.DateOfBirth;
                dbCustomer.DailyTransferLimit = customer.DailyTransferLimit;
                DateAndUserInfoUpdate.UpdateUpdateInfo(dbCustomer, userId);
                await db.SaveChangesAsync();

                return Results.Ok();
            });
        }

        void DeleteMapping()
        {
            app.MapPost("/customer-delete", async ([FromBody] int id, ApplicationContext db) =>
            {
                var customer = db.Customers.FirstOrDefault(x => x.Id == id);
                if (customer == null || customer.DeletedByUserID != null)
                    return Results.Ok();

                DateAndUserInfoUpdate.UpdateDeleteInfo(customer, userId);
                await db.SaveChangesAsync();

                return Results.Ok();
            });
        }

        GetMapping();
        AddMapping();
        EditMapping();
        DeleteMapping();
    }

    void AddBankAccountMapping()
    {
        void GetMapping()
        {
            object CoreBankAccountData(BankAccount bankAccount) => new
            {
                bankAccount.Id,
                bankAccount.AccountNumber,
                bankAccount.CustomerId,
                bankAccount.Balance,
                bankAccount.CreateDate,
                bankAccount.CreatedByUserID,
                bankAccount.UpdateDate,
                bankAccount.UpdatedByUserID,
                bankAccount.DeleteDate,
                bankAccount.DeletedByUserID
            };

            //Customer Get 
            app.MapGet("/bankaccount", (ApplicationContext db) =>
            {
                var bankAccounts = db.BankAccounts.Where(x => x.DeletedByUserID == null).Select(CoreBankAccountData).ToList();
                return Results.Ok(bankAccounts);
            });

            app.MapGet("/customer/{id:int}/bankaccount", (int id, ApplicationContext db) =>
            {
                var dbBankAccount = db.BankAccounts.FirstOrDefault(x => x.CustomerId == id && x.DeletedByUserID == null);
                return dbBankAccount != null ? Results.Ok(CoreBankAccountData(dbBankAccount)) : Results.NotFound();
            });

            app.MapGet("/bankaccount/{id:int}", (int id, ApplicationContext db) =>
            {
                var dbBankAccount = db.BankAccounts.FirstOrDefault(x => x.Id == id);
                return dbBankAccount != null ? Results.Ok(CoreBankAccountData(dbBankAccount)) : Results.NotFound();
            });

            app.MapGet("/bankaccount-all", (ApplicationContext db) =>
            {
                var customers = db.BankAccounts.Select(CoreBankAccountData).ToList();
                return Results.Ok(customers);
            });

            app.MapGet("/bankaccount-deleted", (ApplicationContext db) =>
            {
                var customers = db.BankAccounts.Where(x => x.DeletedByUserID != null).Select(CoreBankAccountData).ToList();
                return Results.Ok(customers);
            });

            app.MapPost("/bankaccount", async ([FromBody] AddBankAccount bankAccount, ApplicationContext db) =>
            {
                var dbCustomer = db.Customers.FirstOrDefault(x => x.Id == bankAccount.CustomerId && x.DeletedByUserID == null);

                if (dbCustomer == null)
                    return Results.NotFound();

                db.BankAccounts.Add((BankAccount)DateAndUserInfoUpdate.UpdateCreateInfo(new BankAccount
                {
                    CustomerId = bankAccount.CustomerId,
                    AccountNumber = bankAccount.AccountNumber,
                    Balance = bankAccount.Balance
                }, userId));

                await db.SaveChangesAsync();

                return Results.Ok();
            });
        }

        void EditMapping()
        {
            app.MapPost("/bankaccount-edit", async ([FromBody] EditBankAccount bankAccount, ApplicationContext db) =>
            {
                var dbBankAccount = db.BankAccounts.FirstOrDefault(x => x.DeletedByUserID == null && x.Id == bankAccount.Id);

                if(dbBankAccount == null)
                    return Results.NotFound();
                
                dbBankAccount.AccountNumber = bankAccount.AccountNumber;
                DateAndUserInfoUpdate.UpdateUpdateInfo(dbBankAccount, userId);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }

        void DeleteMapping()
        {
            app.MapPost("/bankaccount-delete", async ([FromBody] int id, ApplicationContext db) =>
            {
                var dbBankAccount = db.BankAccounts.FirstOrDefault(x => x.DeletedByUserID == null && x.Id == id);
                
                if (dbBankAccount == null)
                    return Results.NotFound();

                DateAndUserInfoUpdate.UpdateDeleteInfo(dbBankAccount, userId);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }

        GetMapping();
        EditMapping();
        DeleteMapping();
    }

    AddCustomerMapping();
    AddBankAccountMapping();
}

MapDataModels();


app.Run();