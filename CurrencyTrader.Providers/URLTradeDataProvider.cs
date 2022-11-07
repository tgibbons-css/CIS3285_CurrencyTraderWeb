using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CurrencyTrader.Contracts;

namespace CurrencyTrader.Providers
{
    public class URLTradeDataProvider : ITradeDataProvider
    {
        public URLTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        public IEnumerable<string> GetTradeData()
        {
            var tradeData = new List<string>();
            logger.LogInfo("Reading trade file from URL: " + url);
            var client = new WebClient();
            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }
            }
            return tradeData;

        }

        // method variables
        string url;
        ILogger logger;
    }
}
