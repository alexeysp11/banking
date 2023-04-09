using Banking.Core.Enums; 
using Banking.Core.Models; 
using Banking.Core.Repo; 

namespace Banking.Core.Preproc
{
    public class TransferPreproc : ICommonTransferPreproc, IEftposTransferPreproc
    {
        private IBankAccountRepo Repo = new CmrRepo(); 

        public bool DepositMoney(int bankAccountId, Money money, Currency currency)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            var bas = Repo.GetBankAccounts(); 
            foreach (var ba in bas) if (ba.GetBankAccountId() == bankAccountId) return ba.DepositMoney(money, currency); 
            throw new System.Exception("Could not find bank account"); 
        }
        public bool WithdrawMoney(int bankAccountId, Money money, Currency currency)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            var bas = Repo.GetBankAccounts(); 
            foreach (var ba in bas) if (ba.GetBankAccountId() == bankAccountId) return ba.WithdrawMoney(money, currency); 
            throw new System.Exception("Could not find bank account"); 
        }
        public bool TransferToBankAccount(int bankAccountId, Money money, Currency currency, string bankAccountNumber)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            var bas = Repo.GetBankAccounts(); 
            foreach (var ba in bas) if (ba.GetBankAccountId() == bankAccountId) return ba.SendToBankAccount(money, currency, bankAccountNumber); 
            throw new System.Exception("Could not find bank account"); 
        }
        public bool TransferToPhoneNumber(int bankAccountId, Money money, Currency currency, string phoneNumber)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            var bas = Repo.GetBankAccounts(); 
            foreach (var ba in bas) if (ba.GetBankAccountId() == bankAccountId) return ba.SendToPhoneNumber(money, currency, phoneNumber); 
            throw new System.Exception("Could not find bank account"); 
        }
        public bool TransferViaFps(int bankAccountId, Money money, Currency currency, string phoneNumber)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            var bas = Repo.GetBankAccounts(); 
            foreach (var ba in bas) if (ba.GetBankAccountId() == bankAccountId) return ba.SendViaFps(money, currency, phoneNumber); 
            throw new System.Exception("Could not find bank account"); 
        }
        public bool TransferToEftpos(int bankAccountId, Money money, Currency currency, string eftposInfo)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            var bas = Repo.GetBankAccounts(); 
            foreach (var ba in bas) if (ba.GetBankAccountId() == bankAccountId) return ba.SendToEftpos(money, currency, eftposInfo); 
            throw new System.Exception("Could not find bank account"); 
        }
    }
}
