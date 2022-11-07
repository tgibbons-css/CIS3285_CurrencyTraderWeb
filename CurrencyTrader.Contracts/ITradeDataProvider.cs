using System.Collections.Generic;

namespace CurrencyTrader.Contracts
{
    public interface ITradeDataProvider
    {
        IEnumerable<string> GetTradeData();
    }
}