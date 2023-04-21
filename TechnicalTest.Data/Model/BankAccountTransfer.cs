using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Data.Model
{
    public class BankAccountTransfer : BaseCreateModel<int>
    {
        public int FromBankAccountId { get; set; }
        public int ToBankAccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Reference { get; set; }
    }
}
