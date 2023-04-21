using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTest.Data.Model;

public class BankAccount : BaseUpdateAndDeleteModel<int>
{
    public int CustomerId { get; set; }
    public required string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    
    public Customer? Customer { get; set; }
    public ICollection<BankAccountFrozenStatus> BankAccountFrozenStatuses { get; set; } = new HashSet<BankAccountFrozenStatus>();
    [InverseProperty("FromBankAccount")]
    public ICollection<BankAccountTransfer> FromBankAccountTransfers { get; set; } = new HashSet<BankAccountTransfer>();
    [InverseProperty("ToBankAccount")]
    public ICollection<BankAccountTransfer> ToBankAccountTransfers { get; set; } = new HashSet<BankAccountTransfer>();
}