namespace TechnicalTest.API.Models;

public record AddCustomerModel(string FirstName, string MiddleNames, string LastName, DateTime DateOfBirth, decimal DailyTransferLimit);
