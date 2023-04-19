namespace TechnicalTest.Data.Model;

public class BankAccount : BaseModel<int>
{
    public required string AccountNumber { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}