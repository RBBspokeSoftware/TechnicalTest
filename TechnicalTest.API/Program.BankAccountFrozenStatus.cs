using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

internal partial class Program
{
    private static void SetGetBankAccountFrozenStatus()
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

        App.MapGet("/bankaccount/{id:int}/FrozenStatus", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccountFrozenStatus.Where(x => x.DeletedByUserId == null && x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        });
        
        App.MapGet("/bankaccount/{id:int}/FrozenStatus-All", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccountFrozenStatus.Where(x => x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        });

        App.MapGet("/bankaccount/{id:int}/FrozenStatus-Deleted", (int id, ApplicationContext db) =>
        {
            var dbBankAccount = db.BankAccountFrozenStatus.Where(x =>  x.DeletedByUserId != null && x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        });
    }
}