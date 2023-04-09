using Banking.Core.Enums; 

namespace Banking.Core.Models
{
    public class Money
    {
        public int Integer { get; private set; }
        public int Fraction { get; private set; }
        public Currency Currency { get; private set; }

        public Money(int integer, int fraction, Currency currency)
        {
            CheckFractionFormat(fraction); 

            Integer = integer; 
            Fraction = fraction; 
            Currency = currency; 
        }

        public void Add(int integer, int fraction)
        {
            Integer += integer; 
            Fraction += fraction; 
            if (Fraction > 99) { Fraction -= 100; Integer += 1; }
        }
        public void Substract(int integer, int fraction)
        {
            Integer -= integer; 
            Fraction -= fraction; 
            if (Fraction < 0) { Fraction += 100; Integer -= 1; }
        }

        public string GetString()
        {
            CheckFractionFormat(Fraction); 
            return Integer.ToString() + "." + Fraction.ToString() + " " + Currency.ToString(); 
        }
        public string GetAmount()
        {
            CheckFractionFormat(Fraction); 
            return Integer.ToString() + "." + Fraction.ToString(); 
        }

        private void CheckFractionFormat(int fraction)
        {
            if (fraction < 0 || fraction > 99) throw new System.Exception("Fraction could not be less than 0 and begger than 99"); 
        }
    }
}