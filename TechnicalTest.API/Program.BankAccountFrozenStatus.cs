using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

internal partial class Program
{
    private static void SetGetBankAccountFrozenStatusMapping()
    {
        object CoreBankAccountFrozenStatusData(BankAccountFrozenStatus bankAccountFrozenStatus) => new
        {
            bankAccountFrozenStatus.Id,
            bankAccountFrozenStatus.Comment,
            bankAccountFrozenStatus.CreateDate,
            bankAccountFrozenStatus.CreatedByUserId,
            bankAccountFrozenStatus.DeleteDate,
            bankAccountFrozenStatus.DeletedByUserId
        };

        App.MapGet("/bankaccount/{id:int}/frozenstatus", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccountFrozenStatuses.Where(x => x.DeletedByUserId == null && x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        });
        
        App.MapGet("/bankaccount/{id:int}/frozenstatus-All", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccountFrozenStatuses.Where(x => x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        });

        App.MapGet("/bankaccount/{id:int}/frozenstatus-Deleted", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccountFrozenStatuses.Where(x =>  x.DeletedByUserId != null && x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        });
    }
    
    private static void SetAddBankAccountFrozenStatusMapping()
    {
        App.MapPost("/bankaccountFrozenStatus", async ([FromBody] AddBankAccountFrozenStatusModel frozenStatus, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccounts.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == frozenStatus.BankAccountId );

            if (dbBankAccount == null)
                return Results.NotFound();

            db.BankAccountFrozenStatuses.Add((BankAccountFrozenStatus)DateAndUserInfoUpdate.UpdateCreateInfo(new BankAccountFrozenStatus
            {
                BankAccountId = frozenStatus.BankAccountId,
                Comment = frozenStatus.Comment
            }, UserId));

            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
    
    private static void SetDeleteBankAccountFrozenStatusMapping()
    {
        App.MapPost("/bankaccountFrozenStatus-delete", async ([FromBody] int id, ApplicationContext db) =>
        {
            var bankaccount = db.BankAccountFrozenStatuses.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == id);
            if (bankaccount == null)
                return Results.Ok();

            DateAndUserInfoUpdate.UpdateDeleteInfo(bankaccount, UserId);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}