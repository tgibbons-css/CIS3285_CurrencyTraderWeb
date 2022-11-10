using System.Collections.Generic;
using System.IO;

using CurrencyTrader.Contracts;

namespace CurrencyTrader.Providers
{
    public class StreamTradeDataProvider : ITradeDataProvider
    {
        public StreamTradeDataProvider(Stream stream, ILogger logger)
        {
            this.stream = stream;
            this.logger = logger;
        }

        public IEnumerable<string> GetTradeData()
        {
            List<string> tradeData = new List<string>();
            logger.LogInfo("Reading trades from file stream.");
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }
            }
            IEnumerable<string> readonlyTradeData = tradeData;
            return readonlyTradeData;
        }

        private readonly Stream stream;
        private readonly ILogger logger;
    }
}
