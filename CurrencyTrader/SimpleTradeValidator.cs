
using CurrencyTrader.Contracts;

namespace CurrencyTrader
{
    public class SimpleTradeValidator : ITradeValidator
    {
        private readonly ILogger logger;

        public SimpleTradeValidator(ILogger logger)
        {
            this.logger = logger;
        }

        public bool Validate(string[] tradeData)
        {
            if (tradeData.Length != 3)
            {
                logger.LogWarning("Line malformed. Only {0} field(s) found.", tradeData.Length);
                return false;
            }

            if (tradeData[0].Length != 6)
            {
                logger.LogWarning("Trade currencies malformed: '{0}'", tradeData[0]);
                return false;
            }

            int tradeAmount;
            if (!int.TryParse(tradeData[1], out tradeAmount))
            {
                logger.LogWarning("Trade not a valid integer: '{0}'", tradeData[1]);
                return false;
            }

            decimal tradePrice;
            if (!decimal.TryParse(tradeData[2], out tradePrice))
            {
                logger.LogWarning("Trade price not a valid decimal: '{0}'", tradeData[2]);
                return false;
            }
            // Request 403 "I want to prevent dangerous trades" 
            if (tradeAmount < MINAMOUNT)
            {
                logger.LogWarning("Trade amount is below minimum of " + MINAMOUNT + ": '{0}'", tradeData[1]);
                return false;
            }

            // Request 403 "I want to prevent dangerous trades" 
            if (tradeAmount > MAXAMOUNT)
            {
                logger.LogWarning("Trade amount is above maximum of " + MAXAMOUNT + ": '{0}'", tradeData[1]);
                return false;
            }

            return true;
        }

        // Request 403 "I want to prevent dangerous trades" 
        private const int MINAMOUNT = 1000;
        private const int MAXAMOUNT = 100000;
    }
}
