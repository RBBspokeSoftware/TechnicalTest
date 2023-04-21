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
    public class CustomerController : ControllerBase
    {
        internal static object CoreCustomerData(Customer customer) => new
        {
            customer.Id,
            customer.FirstName,
            customer.MiddleNames,
            customer.LastName,
            customer.DateOfBirth,
            customer.DailyTransferLimit,
            customer.CreateDate,
            customer.CreatedByUserId,
            customer.UpdateDate,
            customer.UpdatedByUserId,
            customer.DeleteDate,
            customer.DeletedByUserId
        };

        #region Get

        [HttpGet]
        public IResult Get()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var customers = db.Customers.Where(x => x.DeletedByUserId == null).Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        }

        [HttpGet]
        [Route("{id}")]
        public IResult Get(int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccountTransfer = db.Customers.Where(x => x.Id == id).Select(CoreCustomerData).ToList();
            return Results.Ok(dbBankAccountTransfer);
        }

        [HttpGet]
        [Route("All")]
        public IResult GetAll()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var customers = db.Customers.Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        }

        [HttpGet]
        [Route("Deleted")]
        public IResult GetDeleted()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var customers = db.Customers.Where(x => x.DeletedByUserId != null).Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        }
        
        [HttpGet]
        [Route("{id}/BankAccount")]
        public IResult GetBankAccount(int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbBankAccount = db.BankAccounts.Where(x => x.CustomerId == id).Select(BankAccountController.CoreBankAccountData);
            return Results.Ok(dbBankAccount);
        }

        #endregion Get

        [HttpPost]
        public IResult Add([FromBody] AddCustomerModel customer)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            db.Customers.Add((Customer)DateAndUserInfoUpdate.UpdateCreateInfo(new Customer
            {
                FirstName = customer.FirstName,
                MiddleNames = customer.MiddleNames,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                DailyTransferLimit = customer.DailyTransferLimit,
            }, Program.UserId));

            db.SaveChangesAsync();

            return Results.Ok();
        }

        [HttpPost]
        [Route("Edit")]
        public IResult Edit([FromBody] EditCustomerModel customer)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var dbCustomer = db.Customers.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == customer.Id);

            if (dbCustomer == null)
                return Results.NotFound();

            dbCustomer.FirstName = customer.FirstName;
            dbCustomer.MiddleNames = customer.MiddleNames;
            dbCustomer.LastName = customer.LastName;
            dbCustomer.DateOfBirth = customer.DateOfBirth;
            dbCustomer.DailyTransferLimit = customer.DailyTransferLimit;
            DateAndUserInfoUpdate.UpdateUpdateInfo(dbCustomer, Program.UserId);
            db.SaveChangesAsync();

            return Results.Ok();
        }
        
        [HttpPost]
        [Route("Delete")]
        public IResult Delete([FromBody] int id)
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var customer = db.Customers.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == id );
            if (customer == null)
                return Results.Ok();

            DateAndUserInfoUpdate.UpdateDeleteInfo(customer, Program.UserId);
            db.SaveChangesAsync();

            return Results.Ok();
        }
    }
}