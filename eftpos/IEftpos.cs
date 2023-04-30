using Banking.Common.Enums; 
using Banking.Common.Models; 

namespace Banking.Eftpos
{
    public interface IEftpos
    {
        bool StartPayment(); 
        bool FinishPayment(); 
        bool EnterPin(string pin, string eftposUid, string eftposInfo); 
        bool TransferToEftpos(Money money, Currency currency, string eftposUid, string eftposInfo); 
    }
}
