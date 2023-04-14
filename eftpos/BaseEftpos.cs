using Banking.Common.Enums; 
using Banking.Common.Models; 
using System.Collections.Generic;

namespace Banking.Eftpos
{
    public class BaseEftpos : IEftpos
    {
        private string CardNumber { get; set; }
        
        // Start payment: tap, swipe or insert card to make a payment 
        public bool StartPayment()
        {
            CardNumber = "5345-5732-2248"; 
            return true; 
        }

        public bool FinishPayment()
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
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8081/banking/eftpos/v1/pin/enter/", values));

            return true; 
        }

        public bool TransferToEftpos(Money money, Currency currency, string eftposInfo)
        {
            var values = new Dictionary<string, string>
            {
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency() },
                { "eftposinfo", eftposInfo }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post("http://localhost:8081/banking/eftpos/v1/transfer/", values));

            return true; 
        }
    }
}
