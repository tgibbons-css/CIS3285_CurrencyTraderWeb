using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using CurrencyTrader.Contracts;
using CurrencyTrader.Providers;

namespace CurrencyTrader.Tests
{
    [TestClass()]
    public class StreamTradeDataProviderTests
    {

        private int countStrings(IEnumerable<string> collectionOfStrings)
        {
            // count the trades
            int count = 0;
            foreach (var trade in collectionOfStrings)
            {
                count++;
            }
            return count;
        }


        [TestMethod()]
        public void TestLocaFile()
        {
            //Arrange
            ILogger logger = new ConsoleLogger();
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CurrencyTraderTests.trades_1good.txt");

            ITradeDataProvider tradeProvider = new StreamTradeDataProvider(tradeStream, logger);

            //Act
            IEnumerable<string> trades = tradeProvider.GetTradeData();

            //Assert
 
            Assert.AreEqual(countStrings(trades), 1);
        }

    }
}