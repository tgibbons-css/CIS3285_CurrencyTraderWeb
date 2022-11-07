
using CurrencyTrader.Contracts;

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
            var lines = tradeDataProvider.GetTradeData();
            var trades = tradeParser.ParseTrades(lines);
            tradeStorage.StoreTrades(trades);
        }

        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }
}
