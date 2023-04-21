using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

namespace TechnicalTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        internal static  object CoreBankAccountData(BankAccount bankAccount) => new
        {
            bankAccount.Id,
            bankAccount.AccountNumber,
            bankAccount.CustomerId,
            bankAccount.Balance,
            bankAccount.CreateDate,
            bankAccount.CreatedByUserId,
            bankAccount.UpdateDate,
            bankAccount.UpdatedByUserId,
            bankAccount.DeleteDate,
            bankAccount.DeletedByUserId
        };
        
        #region Get
        
        [HttpGet]
        public IResult Get()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var bankaccounts = db.BankAccounts.Where(x => x.DeletedByUserId == null).Select(CoreBankAccountData).ToList();
            return Results.Ok(bankaccounts);
        }

        [HttpGet]
        [Route("{id}")]
        public IResult Get(int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccounts.FirstOrDefault(x => x.Id == id);
            return dbBankAccount != null ? Results.Ok(CoreBankAccountData(dbBankAccount)) : Results.NotFound();
        }
        
        [HttpGet]
        [Route("All")]
        public IResult GetAll()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccounts.Select(CoreBankAccountData).ToList();
            return Results.Ok(dbBankAccount);
        }

        [HttpGet]
        [Route("Deleted")]
        public IResult GetDeleted()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccounts.Where(x => x.DeletedByUserId != null).Select(CoreBankAccountData).ToList();
            return Results.Ok(dbBankAccount);
        }
        
        [HttpGet]
        [Route("{id}/FrozenStatus")]
        public IResult GetFrozenStatus(int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccountFrozenStatuses.Where(x => x.DeletedByUserId == null && x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        }
        
        [HttpGet]
        [Route("{id}/FrozenStatus/All")]
        public IResult GetFrozenStatusAll(int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccountFrozenStatuses.Where(x => x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        }
        
        [HttpGet]
        [Route("{id}/FrozenStatus/Deleted")]
        public IResult GetFrozenStatusDeleted(int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccountFrozenStatuses.Where(x => x.DeletedByUserId != null &&x.BankAccountId == id);
            return dbBankAccount != null ? Results.Ok(dbBankAccount) : Results.NotFound();
        }
        
        #endregion Get
        
        [HttpPost]
        public IResult Add([FromBody] AddBankAccountModel bankAccount)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbCustomer = db.BankAccounts.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == bankAccount.CustomerId );

            if (dbCustomer == null)
                return Results.NotFound();

            db.BankAccounts.Add((BankAccount)DateAndUserInfoUpdate.UpdateCreateInfo(new BankAccount
            {
                CustomerId = bankAccount.CustomerId,
                AccountNumber = bankAccount.AccountNumber,
                Balance = bankAccount.Balance
            }, Program.UserId));

            db.SaveChangesAsync();

            return Results.Ok();
        }

        [HttpPost]
        [Route("Edit")]
        public IResult Edit([FromBody] EditBankAccountModel bankAccount)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccounts.FirstOrDefault(x =>  x.DeletedByUserId == null && x.Id == bankAccount.Id);

            if(dbBankAccount == null)
                return Results.NotFound();
                
            dbBankAccount.AccountNumber = bankAccount.AccountNumber;
            DateAndUserInfoUpdate.UpdateUpdateInfo(dbBankAccount, Program.UserId);
            db.SaveChangesAsync();
            return Results.Ok();
        }
        
        [HttpPost]
        [Route("Delete")]
        public IResult Delete([FromBody] int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var bankaccount = db.BankAccounts.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == id);
            if (bankaccount == null)
                return Results.Ok();

            DateAndUserInfoUpdate.UpdateDeleteInfo(bankaccount, Program.UserId);
            db.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
