namespace TechnicalTest.Data.Model;

public class BankAccountFrozenStatus : BaseDeleteModel<int>
{
    public required string Comment { get; set; }
    public int FrozenByUser { get; set; }
  
    public int BankAccountId { get; set; }
    public BankAccount? BankAccount { get; set; }
}