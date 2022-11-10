
using CurrencyTrader.Contracts;
using System.Collections.Generic;

namespace CurrencyTrader
{
    public class TradeProcessor
    {
        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.tradeDataProvider = tradeDataProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        public void ProcessTrades()
        {
            IEnumerable<string> lines = tradeDataProvider.GetTradeData();
            IEnumerable<TradeRecord> trades = tradeParser.ParseTrades(lines);
            tradeStorage.StoreTrades(trades);
        }

        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }
}
