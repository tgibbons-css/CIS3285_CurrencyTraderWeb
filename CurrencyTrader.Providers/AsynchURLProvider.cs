using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CurrencyTrader.Contracts;

namespace CurrencyTrader.Providers
{
    class AsynchURLProvider : ITradeDataProvider
    {
        ITradeDataProvider baseProvider;

        public AsynchURLProvider(ITradeDataProvider baseProvider)
        {
            this.baseProvider = baseProvider;
        }

        public IEnumerable<string> GetTradeData()
        {
            Task<IEnumerable<string>> task = Task.Run(() => baseProvider.GetTradeData()); ;
            task.Wait();
            IEnumerable<string> lines = task.Result;
            return lines;
        }
    }
}
