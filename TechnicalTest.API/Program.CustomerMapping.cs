using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.Models;
using TechnicalTest.API.Models.DateAndUser;
using TechnicalTest.Data;
using TechnicalTest.Data.Model;

internal partial class Program
{
    private static object CoreCustomerData(Customer customer) => new
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
    
    private static void SetGetCustomerMapping()
    {
        App.MapGet("/customer", (ApplicationContext db) =>
        {
            var customers = db.Customers.Where(x => x.DeletedByUserId == null).Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        });

        App.MapGet("/customer/{id:int}", (int id, ApplicationContext db) =>
        {
            var dbCustomer = db.Customers.FirstOrDefault(x => x.Id == id);

            return dbCustomer != null ? Results.Ok(CoreCustomerData(dbCustomer)) : Results.NotFound();
        });

        App.MapGet("/customer-all", (ApplicationContext db) =>
        {
            var customers = db.Customers.Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        });

        App.MapGet("/customer-deleted", (ApplicationContext db) =>
        {
            var customers = db.Customers.Where(x => x.DeletedByUserId != null).Select(CoreCustomerData).ToList();
            return Results.Ok(customers);
        });
    }

    private static void SetAddCustomerMapping()
    {
        App.MapPost("/customer", async ([FromBody] AddCustomerModel customer, ApplicationContext db) =>
        {
            db.Customers.Add((Customer)DateAndUserInfoUpdate.UpdateCreateInfo(new Customer
            {
                FirstName = customer.FirstName,
                MiddleNames = customer.MiddleNames,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                DailyTransferLimit = customer.DailyTransferLimit,
            }, UserId));

            await db.SaveChangesAsync();

            return Results.Ok();
        });
        
        App.MapPost("/customer-return", async ([FromBody] AddCustomerModel customer, ApplicationContext db) =>
        {
            Customer dbCustomer = new Customer
            {
                FirstName = customer.FirstName,
                MiddleNames = customer.MiddleNames,
                LastName = customer.LastName,
                DateOfBirth = customer.DateOfBirth,
                DailyTransferLimit = customer.DailyTransferLimit,
            };
            db.Customers.Add((Customer)DateAndUserInfoUpdate.UpdateCreateInfo(dbCustomer, UserId));

            await db.SaveChangesAsync();

            return Results.Ok(CoreCustomerData(dbCustomer));
        });
    }
    
    private static void SetEditCustomerMapping()
    {
        App.MapPost("/customer-edit", async ([FromBody] EditCustomerModel customer, ApplicationContext db) =>
        {
            var dbCustomer = db.Customers.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == customer.Id);

            if (dbCustomer == null)
                return Results.NotFound();
                
            dbCustomer.FirstName = customer.FirstName;
            dbCustomer.MiddleNames = customer.MiddleNames;
            dbCustomer.LastName = customer.LastName;
            dbCustomer.DateOfBirth = customer.DateOfBirth;
            dbCustomer.DailyTransferLimit = customer.DailyTransferLimit;
            DateAndUserInfoUpdate.UpdateUpdateInfo(dbCustomer, UserId);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }

    private static void SetDeleteCustomerMapping()
    {
        App.MapPost("/customer-delete", async ([FromBody] int id, ApplicationContext db) =>
        {
            var customer = db.Customers.FirstOrDefault(x => x.DeletedByUserId == null && x.Id == id );
            if (customer == null)
                return Results.Ok();

            DateAndUserInfoUpdate.UpdateDeleteInfo(customer, UserId);
            await db.SaveChangesAsync();

            return Results.Ok();
        });
    }
}