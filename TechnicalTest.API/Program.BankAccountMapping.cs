using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

internal partial class Program
{
    private static void SetGetBankAccountMapping()
    {
        object CoreBankAccountData(BankAccount bankAccount) => new
        {
            bankAccount.Id,
            bankAccount.AccountNumber,
            bankAccount.CustomerId,
            bankAccount.Balance,
            bankAccount.CreateDate,
            CreatedByUserID = bankAccount.CreatedByUserId,
            bankAccount.UpdateDate,
            bankAccount.UpdatedByUserId,
            bankAccount.DeleteDate,
            DeletedByUserID = bankAccount.DeletedByUserId
        };

        App.MapGet("/bankaccount", (ApplicationContext db) =>
        {
            var bankaccounts = db.BankAccounts.Where(x => x.DeletedByUserId == null).Select(CoreBankAccountData).ToList();
            return Results.Ok(bankaccounts);
        });
        
        App.MapGet("customer/{id:int}/bankaccount", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccounts.Where(x => x.CustomerId == id).Select(CoreBankAccountData);

            return Results.Ok(dbBankAccount);
        });

        App.MapGet("/bankaccount/{id:int}", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccounts.FirstOrDefault(x => x.Id == id);

            return dbBankAccount != null ? Results.Ok(CoreBankAccountData(dbBankAccount)) : Results.NotFound();
        });

        App.MapGet("/bankaccount-all", (ApplicationContext db) =>
        {
            var bankaccounts = db.BankAccounts.Select(CoreBankAccountData).ToList();
            return Results.Ok(bankaccounts);
        });

        App.MapGet("/bankaccount-deleted", (ApplicationContext db) =>
        {
            var bankaccounts = db.BankAccounts.Where(x => x.DeletedByUserId != null).Select(CoreBankAccountData).ToList();
            return Results.Ok(bankaccounts);
        });
    }

    private static void SetAddBankAccountMapping()
    {
        App.MapPost("/bankaccount", async ([FromBody] AddBankAccountModel bankAccount, ApplicationContext db) =>
        {
            var dbCustomer = db.Customers.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == bankAccount.CustomerId );

            if (dbCustomer == null)
                return Results.NotFound();

            db.BankAccounts.Add((BankAccount)DateAndUserInfoUpdate.UpdateCreateInfo(new BankAccount
            {
                CustomerId = bankAccount.CustomerId,
                AccountNumber = bankAccount.AccountNumber,
                Balance = bankAccount.Balance
            }, UserId));

            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
    
    private static void SetEditBankAccountMapping()
    {
        App.MapPost("/bankaccount-edit", async ([FromBody] EditBankAccountModel bankAccount, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccounts.FirstOrDefault(x =>  x.DeletedByUserId == null && x.Id == bankAccount.Id);

            if(dbBankAccount == null)
                return Results.NotFound();
                
            dbBankAccount.AccountNumber = bankAccount.AccountNumber;
            DateAndUserInfoUpdate.UpdateUpdateInfo(dbBankAccount, UserId);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
    }

    private static void SetDeleteBankAccountMapping()
    {
        App.MapPost("/bankaccount-delete", async ([FromBody] int id, ApplicationContext db) =>
        {
            var bankaccount = db.BankAccounts.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == id);
            if (bankaccount == null)
                return Results.Ok();

            DateAndUserInfoUpdate.UpdateDeleteInfo(bankaccount, UserId);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}