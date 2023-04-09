using Banking.Core.Enums; 
using Banking.Core.Models; 

namespace Banking.Eftpos
{
    public interface IEftpos
    {
        bool StartPayment(); 
        bool FinishPayment(); 
        bool EnterPin(string pin); 
        bool TransferToEftpos(Money money, Currency currency, string eftposInfo); 
    }
}
