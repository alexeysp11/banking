using Banking.Common.Enums; 
using Banking.Common.Models; 
using Banking.Core.Preproc; 

namespace Banking.CoreServer
{
    public class CoreRouter
    {
        private string CardNumber { get; set; }

        private CardPreproc CardPreproc { get; set; }
        private BankAccountPreproc BankAccountPreproc { get; set; }
        private ICommonTransferPreproc CommonTransferPreproc { get; set; }
        private IEftposTransferPreproc EftposTransferPreproc { get; set; }

        public CoreRouter()
        {
            CardPreproc = new CardPreproc(); 
            BankAccountPreproc = new BankAccountPreproc(); 
            CommonTransferPreproc = new TransferPreproc(); 
            EftposTransferPreproc = new TransferPreproc(); 
        }

        #region ATM 
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
            return CommonTransferPreproc.DepositMoney(CardPreproc.GetBankAccountId(CardNumber), money, currency); 
        }
        public bool WithdrawMoney(Money money, Currency currency)
        {
            return CommonTransferPreproc.WithdrawMoney(CardPreproc.GetBankAccountId(CardNumber), money, currency); 
        }

        public bool TransferToBankAccount(Money money, Currency currency, string bankAccountNumber)
        {
            return CommonTransferPreproc.TransferToBankAccount(CardPreproc.GetBankAccountId(CardNumber), money, currency, bankAccountNumber); 
        }
        public bool TransferToPhoneNumber(Money money, Currency currency, string phoneNumber)
        {
            return CommonTransferPreproc.TransferToPhoneNumber(CardPreproc.GetBankAccountId(CardNumber), money, currency, phoneNumber); 
        }
        public bool TransferViaFps(Money money, Currency currency, string phoneNumber)
        {
            return CommonTransferPreproc.TransferViaFps(CardPreproc.GetBankAccountId(CardNumber), money, currency, phoneNumber); 
        }
        #endregion  // ATM 
        
        #region EFTPOS

        public bool TransferToEftpos(Money money, Currency currency, string eftposInfo)
        {
            return EftposTransferPreproc.TransferToEftpos(CardPreproc.GetBankAccountId(CardNumber), money, currency, eftposInfo); 
        }
        #endregion  // EFTPOS
    }
}
