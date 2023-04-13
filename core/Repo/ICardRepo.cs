using System.Collections.Generic; 
using Banking.Common.Models; 

namespace Banking.Core.Repo
{
    public interface ICardRepo
    {
        List<Card> GetCards(); 
    }
}
