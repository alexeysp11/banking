using System.Collections.Generic; 
using Banking.Common.Enums; 
using Banking.Common.Models; 

namespace Banking.Atm
{
    public class BaseAtm : IAtm
    {
        private string ServerAddress { get; set; } = "http://localhost:8081/banking/"; 
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

        public bool EnterPin(string pin, string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "atmuid", atmUid }, 
                { "cardnumber", CardNumber },
                { "pin", pin }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/pin/enter/", values));

            return true; 
        }
        public bool ChangePin(string oldPin, string newPin, string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "atmuid", atmUid }, 
                { "cardnumber", CardNumber },
                { "oldpin", oldPin },
                { "newpin", newPin }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/pin/change/", values));

            return true; 
        }
        
        public string CheckBalance(string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "atmuid", atmUid }, 
                { "cardnumber", CardNumber }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/balance/get/", values));

            return "Balance: "; 
        }

        public bool DepositMoney(Money money, Currency currency, string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "atmuid", atmUid }, 
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency().ToLower() }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/cash/deposit/", values));

            return true; 
        }
        public bool WithdrawMoney(Money money, Currency currency, string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency().ToLower() }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/cash/withdraw/", values));

            return true; 
        }

        public bool TransferToBankAccount(Money money, Currency currency, string bankAccountNumber, string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "atmuid", atmUid }, 
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency().ToLower() },
                { "bankaccountnumber", bankAccountNumber }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/transfer/tobankaccount/", values));

            return true; 
        }
        public bool TransferToPhoneNumber(Money money, Currency currency, string phoneNumber, string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "atmuid", atmUid }, 
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency().ToLower() },
                { "phonenumber", phoneNumber.Replace("+", "") }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/transfer/tophonenumber/", values));

            return true; 
        }
        public bool TransferViaFps(Money money, Currency currency, string phoneNumber, string atmUid)
        {
            var values = new Dictionary<string, string>
            {
                { "atmuid", atmUid }, 
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency().ToLower() },
                { "phonenumber", phoneNumber.Replace("+", "") }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "atm/" + atmUid + "/transfer/fps/", values));

            return true; 
        }
    }
}
