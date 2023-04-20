namespace TechnicalTest.Data.Model;

public class BankAccount : BaseModel<int>
{
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public required string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public ICollection<BankAccountFrozenStatus> Status { get; set; } = new HashSet<BankAccountFrozenStatus>();
}