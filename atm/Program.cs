using Banking.Common.Enums; 
using Banking.Common.Models; 

namespace Banking.Atm
{
    class Program
    {
        static void Main(string[] args)
        {
            string atmUid = "v1", oldPin = "5544", newPin = "2343"; 

            Money moneyDepositUsd = new Money(27, 0, Currency.USD); 
            Money moneyDepositEur = new Money(53, 99, Currency.EUR); 
            Money moneyWithdrawalUsd = new Money(18, 65, Currency.USD); 
            Money moneyWithdrawalEur = new Money(52, 39, Currency.EUR); 

            string bankAccountNumber = "TESTACCOUNT-7298635673"; 
            string phoneNumber = "+1-7832-892364"; 
            Money moneyTbaUsd = new Money(23, 90, Currency.USD); 
            Money moneyTbaEur = new Money(9, 0, Currency.EUR); 
            Money moneyTpnUsd = new Money(4, 63, Currency.USD); 
            Money moneyTpnEur = new Money(1, 34, Currency.EUR); 
            Money moneyFpsUsd = new Money(8, 52, Currency.USD); 
            Money moneyFpsEur = new Money(3, 31, Currency.EUR); 
            
            IAtm atm = new BaseAtm(); 

            System.Console.WriteLine("ATM imitation\n".ToUpper());
            try
            {
                bool isInserted = atm.InsertCard(); 
                System.Console.WriteLine(isInserted ? "Card inserted" : "Card is not inserted");
                if (!isInserted) return; 

                bool isPinCorrect = atm.EnterPin(oldPin, atmUid); 
                System.Console.WriteLine("Enter PIN: **** - " + (isPinCorrect ? "OK" : "Incorrect PIN")); 
                if (!isPinCorrect) throw new System.Exception("Incorrect PIN"); 

                System.Console.WriteLine(atm.CheckBalance(atmUid)); 

                System.Console.WriteLine("Deposit: " + moneyDepositUsd.GetString() + " and " + moneyDepositEur.GetString()); 
                atm.DepositMoney(moneyDepositUsd, Currency.USD, atmUid); 
                atm.DepositMoney(moneyDepositEur, Currency.EUR, atmUid); 
                System.Console.WriteLine(atm.CheckBalance(atmUid)); 

                System.Console.WriteLine("Withdraw: " + moneyWithdrawalUsd.GetString() + " and " + moneyWithdrawalEur.GetString()); 
                atm.WithdrawMoney(moneyWithdrawalUsd, Currency.USD, atmUid); 
                atm.WithdrawMoney(moneyWithdrawalEur, Currency.EUR, atmUid); 
                System.Console.WriteLine(atm.CheckBalance(atmUid)); 

                System.Console.WriteLine("Transfer to bank account '" + bankAccountNumber.ToString() + "': " + moneyTbaUsd.GetString() + " and " + moneyTbaEur.GetString()); 
                atm.TransferToBankAccount(moneyTbaUsd, Currency.USD, bankAccountNumber, atmUid); 
                atm.TransferToBankAccount(moneyTbaEur, Currency.EUR, bankAccountNumber, atmUid); 
                System.Console.WriteLine(atm.CheckBalance(atmUid)); 

                System.Console.WriteLine("Transfer to phone number '" + phoneNumber.ToString() + "': " + moneyTpnUsd.GetString() + " and " + moneyTpnEur.GetString()); 
                atm.TransferToPhoneNumber(moneyTpnUsd, Currency.USD, phoneNumber, atmUid); 
                atm.TransferToPhoneNumber(moneyTpnEur, Currency.EUR, phoneNumber, atmUid); 
                System.Console.WriteLine(atm.CheckBalance(atmUid)); 

                System.Console.WriteLine("Transfer to phone number '" + phoneNumber.ToString() + "' via FPS: " + moneyFpsUsd.GetString() + " and " + moneyFpsEur.GetString()); 
                atm.TransferViaFps(moneyFpsUsd, Currency.USD, phoneNumber, atmUid); 
                atm.TransferViaFps(moneyFpsEur, Currency.EUR, phoneNumber, atmUid); 
                System.Console.WriteLine(atm.CheckBalance(atmUid)); 

                System.Console.WriteLine("New PIN: **** - " + (atm.ChangePin(oldPin, newPin, atmUid) ? "OK" : "Unable to change PIN"));
                System.Console.WriteLine("Confirm PIN: **** - " + (atm.EnterPin(newPin, atmUid) ? "OK" : "Incorrect PIN"));
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString()); 
            }
            System.Console.WriteLine(atm.TakeBackCard() ? "Card is taken back" : "Card is still in the ATM");
        }
    }
}
