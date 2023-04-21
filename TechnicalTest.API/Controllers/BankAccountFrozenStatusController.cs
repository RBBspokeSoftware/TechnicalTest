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
    public class BankAccountFrozenStatusController : ControllerBase
    {
        public static object CoreBankAccountFrozenStatusTransferData(BankAccountFrozenStatus bankAccountFrozenStatus) => new
        {
            bankAccountFrozenStatus.Id,
            bankAccountFrozenStatus.Comment,
            bankAccountFrozenStatus.CreateDate,
            bankAccountFrozenStatus.CreatedByUserId,
            bankAccountFrozenStatus.DeleteDate,
            bankAccountFrozenStatus.DeletedByUserId
        };
        
        [HttpPost]
        public IResult Add([FromBody] AddBankAccountFrozenStatusModel frozenStatus)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount =
                db.BankAccounts.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == frozenStatus.BankAccountId);

            if (dbBankAccount == null)
                return Results.NotFound();

            db.BankAccountFrozenStatuses.Add((BankAccountFrozenStatus)DateAndUserInfoUpdate.UpdateCreateInfo(
                new BankAccountFrozenStatus
                {
                    BankAccountId = frozenStatus.BankAccountId,
                    Comment = frozenStatus.Comment
                }, Program.UserId));

            db.SaveChangesAsync();

            return Results.Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public IResult Add([FromBody] int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var bankaccount = db.BankAccountFrozenStatuses.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == id);
            if (bankaccount == null)
                return Results.Ok();

            DateAndUserInfoUpdate.UpdateDeleteInfo(bankaccount, Program.UserId);
            db.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
