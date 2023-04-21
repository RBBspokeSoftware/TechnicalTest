using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

internal partial class Program
{
    private static void SetGetBankAccountTransferMapping()
    {
        object CoreBankAccountTransferData(BankAccountTransfer bankAccountTransfer) => new
        {
            bankAccountTransfer.Id,
            bankAccountTransfer.Amount,
            bankAccountTransfer.Reference,
            bankAccountTransfer.CreateDate,
            bankAccountTransfer.CreatedByUserId
        };

        App.MapGet("/BankAccount/{id:int}/transfer", (int id, ApplicationContext db) =>
        {
            var dbBankAccountTransfer = db.BankAccountTransfers.Where(x => x.FromBankAccountId == id).Select(CoreBankAccountTransferData).ToList();

            return Results.Ok(dbBankAccountTransfer);
        });
    }

    private static void SetAddBankAccountTransferMapping()
    {
        App.MapPost("/bankAccountTransfer", async ([FromBody] AddBankAccountTransferModel bankAccountTransfer, ApplicationContext db) =>
        {
            BankAccount GetBankAccount(int id) => db.BankAccounts.FirstOrDefault(x =>
                x.DeletedByUserId == null && x.Id == id);
            
            bool AccountIsFrozen(BankAccount bankAccount) => db.BankAccountFrozenStatuses.FirstOrDefault(x =>
                x.DeletedByUserId == null && x.BankAccountId == bankAccount.Id) != null;
            
            var dbFromBankAccount = GetBankAccount(bankAccountTransfer.FromBankAccountId);
            
            if(dbFromBankAccount == null || dbFromBankAccount.Balance < bankAccountTransfer.Amount || AccountIsFrozen(dbFromBankAccount) )
                return Results.BadRequest();

            var dbCustomer = db.Customers.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == dbFromBankAccount.CustomerId);
            
            if(dbCustomer == null)
                return Results.BadRequest();
            
            var dbToBankAccount = db.BankAccounts.FirstOrDefault(x =>
                x.DeletedByUserId == null && x.CustomerId == dbCustomer.Id && x.Id == bankAccountTransfer.ToBankAccountId);
            
            if(dbToBankAccount == null || AccountIsFrozen(dbToBankAccount))
                return Results.BadRequest();

            var customerBankAccountIds = db.BankAccounts.Where(x => x.DeletedByUserId == null && x.CustomerId == dbFromBankAccount.CustomerId)
                .Select(x => x.Id).ToArray();

            var amountCustomerHasTransferredToday = db.BankAccountTransfers
                .Where(x => customerBankAccountIds.Contains(x.FromBankAccountId) && x.CreateDate >= DateTime.Today)
                .Select(x => x.Amount).ToArray().Sum();
            
            if(amountCustomerHasTransferredToday + bankAccountTransfer.Amount > dbCustomer.DailyTransferLimit)
                return Results.BadRequest();

            dbFromBankAccount.Balance -= bankAccountTransfer.Amount;
            dbToBankAccount.Balance += bankAccountTransfer.Amount;

            db.BankAccountTransfers.Add((BankAccountTransfer)DateAndUserInfoUpdate.UpdateCreateInfo(new BankAccountTransfer
            {
                FromBankAccountId = bankAccountTransfer.FromBankAccountId,
                ToBankAccountId = bankAccountTransfer.ToBankAccountId,
                Reference = bankAccountTransfer.Reference,
                Amount = bankAccountTransfer.Amount
            }, UserId));

            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}