using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using CurrencyTrader.Contracts;

namespace CurrencyTrader.Tests
{
    [TestClass()]
    public class SimpleTradeMapperTests
    {
        [TestMethod()]
        public void TestGoodPrice()
        {
            //Arrange
            var mapper = new SimpleTradeMapper();
            string[] strData = { "AAABBB", "4444", "999.00" };

            //Act
            TradeRecord trade = mapper.Map(strData);

            //Assert
            Assert.AreEqual(trade.Price, 999.00M);
        }

        [TestMethod()]
        public void TestGoodLotAmount()
        {
            //Arrange
            var mapper = new SimpleTradeMapper();
            string[] strData = { "AAABBB", "44000", "999.00" };
            float LotSize = 1000f;

            //Act
            TradeRecord trade = mapper.Map(strData);

            //Assert
            Assert.AreEqual(trade.Lots, 44000/ LotSize);
        }

        [TestMethod()]
        public void TestGoodSourceCurrency()
        {
            //Arrange
            var mapper = new SimpleTradeMapper();
            string[] strData = { "AAABBB", "4444", "999.00" };

            //Act
            TradeRecord trade = mapper.Map(strData);

            //Assert
            Assert.AreEqual(trade.SourceCurrency, "AAA");
        }

        [TestMethod()]
        public void TestGoodDestinationCurrency()
        {
            //Arrange
            var mapper = new SimpleTradeMapper();
            string[] strData = { "AAABBB", "4444", "999.00" };

            //Act
            TradeRecord trade = mapper.Map(strData);

            //Assert
            Assert.AreEqual(trade.DestinationCurrency, "BBB");
        }

    }
}