namespace CurrencyTrader.Contracts
{
    public interface ITradeValidator
    {
        bool Validate(string[] tradeData);
    }
}