using System.Collections.Generic; 
using Banking.Common.Enums; 
using Banking.Common.Models; 

namespace Banking.Atm
{
    public class BaseAtm : IAtm
    {
        private string CardNumber { get; set; }

        public bool InsertCard()
        {
            CardNumber = "5345-5732-2248"; 
            return true; 
        }
        public bool TakeBackCard()
        {
            CardNumber = string.Empty; 
            return true; 
        }

        public bool EnterPin(string pin)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "pin", pin }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/pin/enter/", values));

            return true; 
        }
        public bool ChangePin(string oldPin, string newPin)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "oldpin", oldPin },
                { "newpin", newPin }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/pin/change/", values));

            return true; 
        }
        
        public string CheckBalance()
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/balance/get/", values));

            return "Balance: "; 
        }

        public bool DepositMoney(Money money, Currency currency)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency() }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/cash/deposit/", values));

            return true; 
        }
        public bool WithdrawMoney(Money money, Currency currency)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency() }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/cash/withdraw/", values));

            return true; 
        }

        public bool TransferToBankAccount(Money money, Currency currency, string bankAccountNumber)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency() },
                { "bankaccountnumber", bankAccountNumber }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/transfer/tobankaccount/", values));

            return true; 
        }
        public bool TransferToPhoneNumber(Money money, Currency currency, string phoneNumber)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency() },
                { "phonenumber", phoneNumber.Replace("+", "") }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/transfer/tophonenumber/", values));

            return true; 
        }
        public bool TransferViaFps(Money money, Currency currency, string phoneNumber)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency() },
                { "phonenumber", phoneNumber.Replace("+", "") }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8080/banking/atm/v1/transfer/fps/", values));

            return true; 
        }
    }
}
