using System.Collections.Generic; 
using Banking.Common.Models; 

namespace Banking.Core.Repo
{
    public interface IBankAccountRepo
    {
        List<BankAccount> GetBankAccounts();
    }
}
