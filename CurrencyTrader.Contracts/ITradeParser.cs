using System.Collections.Generic;

namespace CurrencyTrader.Contracts
{
    public interface ITradeParser
    {
        IEnumerable<TradeRecord> ParseTrades(IEnumerable<string> tradeData);
    }
}