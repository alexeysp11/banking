using Banking.Core.Enums; 
using Banking.Core.Models; 

namespace Banking.Atm
{
    public interface IAtm
    {
        bool InsertCard(); 
        bool TakeBackCard(); 

        bool EnterPin(string pin); 
        bool ChangePin(string oldPin, string newPin); 

        string CheckBalance(); 

        bool DepositMoney(Money money, Currency currency); 
        bool WithdrawMoney(Money money, Currency currency); 

        bool TransferToBankAccount(Money money, Currency currency, string bankAccountNumber); 
        bool TransferToPhoneNumber(Money money, Currency currency, string phoneNumber); 
        bool TransferViaFps(Money money, Currency currency, string phoneNumber); 
    }
}
