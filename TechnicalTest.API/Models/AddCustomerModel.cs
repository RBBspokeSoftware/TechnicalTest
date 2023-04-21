namespace TechnicalTest.API.Models;

public record AddCustomerModel(string FirstName, string MiddleNames, string LastName, DateTime DateOfBirth, decimal DailyTransferLimit);

public record EditCustomerModel(int Id, string FirstName, string MiddleNames, string LastName, DateTime DateOfBirth, decimal DailyTransferLimit);

public record AddBankAccount(int CustomerId, string AccountNumber, decimal Balance);

public record EditBankAccount(int Id, string AccountNumber);
