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

            try 
            {
                return GetBankAccount(bankAccountId).DepositMoney(money, currency); 
            }
            catch (System.Exception)
            {
                return false; 
            }
        }
        public bool WithdrawMoney(int bankAccountId, Money money, Currency currency)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            try 
            {
                return GetBankAccount(bankAccountId).WithdrawMoney(money, currency); 
            }
            catch (System.Exception)
            {
                return false; 
            }
        }
        public bool TransferToBankAccount(int bankAccountId, Money money, Currency currency, string bankAccountNumber)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            try 
            {
                return GetBankAccount(bankAccountId).SendToBankAccount(money, currency, bankAccountNumber); 
            }
            catch (System.Exception)
            {
                return false; 
            }
        }
        public bool TransferToPhoneNumber(int bankAccountId, Money money, Currency currency, string phoneNumber)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            try 
            {
                return GetBankAccount(bankAccountId).SendToPhoneNumber(money, currency, phoneNumber); 
            }
            catch (System.Exception)
            {
                return false; 
            }
        }
        public bool TransferViaFps(int bankAccountId, Money money, Currency currency, string phoneNumber)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            try 
            {
                return GetBankAccount(bankAccountId).SendViaFps(money, currency, phoneNumber); 
            }
            catch (System.Exception)
            {
                return false; 
            }
        }
        public bool TransferToEftpos(int bankAccountId, Money money, Currency currency, string eftposInfo)
        {
            if (bankAccountId < 0) throw new System.Exception("Bank account could not be negative"); 

            try 
            {
                return GetBankAccount(bankAccountId).SendToEftpos(money, currency, eftposInfo); 
            }
            catch (System.Exception)
            {
                return false; 
            }
        }

        private BankAccount GetBankAccount(int bankAccountId)
        {
            var bas = Repo.GetBankAccounts(); 
            foreach (var ba in bas) if (ba.GetBankAccountId() == bankAccountId) return ba; 
            throw new System.Exception("Could not find bank account"); 
        } 
    }
}
