using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

namespace TechnicalTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private object CoreCustomerData(Customer customer) => new
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
            return  Results.Ok(dbBankAccountTransfer);
        }
        
        [HttpGet]
        [Route("all")]
        public IResult GetAll()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var customers = db.Customers.Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        }
        
        [HttpGet]
        [Route("deleted")]
        public IResult GetDeleted()
        {
            using var scope = Program.App.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            var customers = db.Customers.Where(x => x.DeletedByUserId != null).Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        }
        
        #endregion Get
    }
}
