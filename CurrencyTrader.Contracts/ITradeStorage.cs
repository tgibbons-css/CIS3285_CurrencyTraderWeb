using System.Collections.Generic;

namespace CurrencyTrader.Contracts
{
    public interface ITradeStorage
    {
        void StoreTrades(IEnumerable<TradeRecord> trades);
    }
}