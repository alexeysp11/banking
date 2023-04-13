using Banking.Common.Enums; 
using Banking.Common.Models; 
using Banking.Core.Preproc; 

namespace Banking.Eftpos
{
    public class BaseEftpos : IEftpos
    {
        private string CardNumber { get; set; }

        private CardPreproc CardPreproc { get; set; }
        private IEftposTransferPreproc TransferPreproc { get; set; }
        
        public BaseEftpos()
        {
            CardPreproc = new CardPreproc(); 
            TransferPreproc = new TransferPreproc(); 
        }

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
            return CardPreproc.CheckPin(CardNumber, pin); 
        }

        public bool TransferToEftpos(Money money, Currency currency, string eftposInfo)
        {
            return TransferPreproc.TransferToEftpos(CardPreproc.GetBankAccountId(CardNumber), money, currency, eftposInfo); 
        }
    }
}
