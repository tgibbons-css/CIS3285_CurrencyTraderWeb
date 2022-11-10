using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyTrader.Contracts;

namespace CurrencyTrader.Providers
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        // This method requires NuGet packages System.Net.Http.Formatting.Extension and Newtonsoft.Json
        async Task<IEnumerable<string>> GetProductAsync()
        {
            logger.LogInfo("Connecting the Restful server using HTTP at url: "+url);
            List<string> tradesString = null;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                tradesString = await response.Content.ReadAsAsync<List<string>>();
                logger.LogInfo("Received trade strings of length = " + tradesString.Count);

            }
            return tradesString;
        }

        public IEnumerable<string> GetTradeData()
        {
            try
            {
                var task = Task.Run(() => GetProductAsync());
                task.Wait();
                IEnumerable<string> tradeList = task.Result;
                return tradeList;
            }
            catch (Exception e)
            {
                logger.LogInfo("Exception connecting to the Restful API: " + e.Message);
                logger.LogInfo("Could not read trades from "+ url.ToString());
                IEnumerable<string> emptyList = new List<string>();
                return emptyList;
            }

        }

        // method variables
        string url;
        ILogger logger;
        HttpClient client = new HttpClient();
    }
}
