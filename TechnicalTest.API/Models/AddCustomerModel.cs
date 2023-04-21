namespace TechnicalTest.API.Models;

public record AddCustomerModel(string FirstName, string MiddleNames, string LastName, DateTime DateOfBirth, decimal DailyTransferLimit);

public record EditCustomerModel(int Id, string FirstName, string MiddleNames, string LastName, DateTime DateOfBirth, decimal DailyTransferLimit);

public record AddBankAccountModel(int CustomerId, string AccountNumber, decimal Balance);

public record EditBankAccountModel(int Id, string AccountNumber);

public record AddBankAccountFrozenStatusModel(int CustomerId, string AccountNumber, decimal Balance);
