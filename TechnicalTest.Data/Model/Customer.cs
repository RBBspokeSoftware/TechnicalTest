namespace TechnicalTest.Data.Model;

public class Customer : BaseModel<int>
{
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public ICollection<BankAccount> BankAccounts { get; set; } = new HashSet<BankAccount>();

    public decimal DailyTransferLimit { get; set; }
}