using Banking.Core.Enums; 
using Banking.Core.Models; 
using Banking.Core.Preproc; 

namespace Banking.CoreServer
{
    public class CoreRouter
    {
        private string CardNumber { get; set; }

        private CardPreproc CardPreproc { get; set; }
        private BankAccountPreproc BankAccountPreproc { get; set; }
        private ICommonTransferPreproc TransferPreproc { get; set; }

        public CoreRouter()
        {
            CardPreproc = new CardPreproc(); 
            BankAccountPreproc = new BankAccountPreproc(); 
            TransferPreproc = new TransferPreproc(); 
        }

        public bool EnterPin(string pin)
        {
            return CardPreproc.CheckPin(CardNumber, pin); 
        }
        public bool ChangePin(string oldPin, string newPin)
        {
            return CardPreproc.ChangePin(CardNumber, oldPin, newPin); 
        }
        
        public string CheckBalance()
        {
            return BankAccountPreproc.CheckBalance(CardPreproc.GetBankAccountId(CardNumber)); 
        }

        public bool DepositMoney(Money money, Currency currency)
        {
            return TransferPreproc.DepositMoney(CardPreproc.GetBankAccountId(CardNumber), money, currency); 
        }
        public bool WithdrawMoney(Money money, Currency currency)
        {
            return TransferPreproc.WithdrawMoney(CardPreproc.GetBankAccountId(CardNumber), money, currency); 
        }

        public bool TransferToBankAccount(Money money, Currency currency, string bankAccountNumber)
        {
            return TransferPreproc.TransferToBankAccount(CardPreproc.GetBankAccountId(CardNumber), money, currency, bankAccountNumber); 
        }
        public bool TransferToPhoneNumber(Money money, Currency currency, string phoneNumber)
        {
            return TransferPreproc.TransferToPhoneNumber(CardPreproc.GetBankAccountId(CardNumber), money, currency, phoneNumber); 
        }
        public bool TransferViaFps(Money money, Currency currency, string phoneNumber)
        {
            return TransferPreproc.TransferViaFps(CardPreproc.GetBankAccountId(CardNumber), money, currency, phoneNumber); 
        }
    }
}
