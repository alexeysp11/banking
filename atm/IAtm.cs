using Banking.Common.Enums; 
using Banking.Common.Models; 

namespace Banking.Atm
{
    public interface IAtm
    {
        bool InsertCard(); 
        bool TakeBackCard(); 

        bool EnterPin(string pin, string atmUid); 
        bool ChangePin(string oldPin, string newPin, string atmUid); 

        string CheckBalance(string atmUid); 

        bool DepositMoney(Money money, Currency currency, string atmUid); 
        bool WithdrawMoney(Money money, Currency currency, string atmUid); 

        bool TransferToBankAccount(Money money, Currency currency, string bankAccountNumber, string atmUid); 
        bool TransferToPhoneNumber(Money money, Currency currency, string phoneNumber, string atmUid); 
        bool TransferViaFps(Money money, Currency currency, string phoneNumber, string atmUid); 
    }
}
