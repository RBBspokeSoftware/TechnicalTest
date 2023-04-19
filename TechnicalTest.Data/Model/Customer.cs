namespace TechnicalTest.Data.Model;

public class Customer : BaseModel<int>
{
    public required string FirstName { get; set; }
    public string MiddleNames { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public ICollection<BankAccount> BankAccounts { get; set; } = new HashSet<BankAccount>();

    public required decimal DailyTransferLimit { get; set; }
}