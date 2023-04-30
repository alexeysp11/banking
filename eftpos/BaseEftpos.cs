using Banking.Common.Enums; 
using Banking.Common.Models; 
using System.Collections.Generic;

namespace Banking.Eftpos
{
    public class BaseEftpos : IEftpos
    {
        private string ServerAddress { get; set; } = "http://localhost:8081/banking/"; 
        private string CardNumber { get; set; }
        
        /// <summary>
        /// Start payment: tap, swipe or insert card to make a payment 
        /// </summary>
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

        public bool EnterPin(string pin, string eftposUid, string eftposInfo)
        {
            var values = new Dictionary<string, string>
            {
                { "eftposuid", eftposUid },
                { "cardnumber", CardNumber },
                { "pin", pin },
                { "eftposinfo", eftposInfo }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "eftpos/" + eftposUid + "/pin/enter/", values));

            return true; 
        }

        public bool TransferToEftpos(Money money, Currency currency, string eftposUid, string eftposInfo)
        {
            var values = new Dictionary<string, string>
            {
                { "eftposuid", eftposUid },
                { "cardnumber", CardNumber },
                { "amount", money.GetAmount() },
                { "currency", money.GetCurrency().ToLower() },
                { "eftposinfo", eftposInfo }
            };
            System.Console.WriteLine(Banking.Network.BankingHttpClient.Post(ServerAddress + "eftpos/" + eftposUid + "/transfer/", values));

            return true; 
        }
    }
}
