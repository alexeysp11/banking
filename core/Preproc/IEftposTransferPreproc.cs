using Banking.Core.Enums; 
using Banking.Core.Models; 

namespace Banking.Core.Preproc
{
    public interface IEftposTransferPreproc
    {
        bool TransferToEftpos(int bankAccountId, Money money, Currency currency, string eftposInfo); 
    }
}
