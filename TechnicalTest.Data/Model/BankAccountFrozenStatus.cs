namespace TechnicalTest.Data.Model;

public class BankAccountFrozenStatus : BaseModelCreate<int>
{
    public bool IsFrozen  { get; set; }
    public int FrozenByUser { get; set; }
    public required string Comment { get; set; }
    public int BankAccountId { get; set; }
    public BankAccount? BankAccount { get; set; }
}