namespace CurrencyTrader.Contracts
{
    public interface ITradeMapper
    {
        TradeRecord Map(string[] fields);
    }
}