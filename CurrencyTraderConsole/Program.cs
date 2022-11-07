using System;
using System.IO;
using System.Reflection;
using CurrencyTrader;
using CurrencyTrader.AdoNet;
using CurrencyTrader.Contracts;
using CurrencyTrader.Providers;

namespace CurrencyTraderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // data file to read from locally
            Stream tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Unit9_Trader.trades.txt");
            // URL to read trade file from
            string tradeURL = "http://faculty.css.edu/tgibbons/trades4.txt";
            //Two different URLs for Restful API
            string localRestfulURL = "http://localhost:22359/api/TradeData";
            string azureRestfulURL = "http://unit9trader.azurewebsites.net/api/TradeData";

            ILogger logger = new ConsoleLogger();
            ITradeValidator tradeValidator = new SimpleTradeValidator(logger);

            //These are three different trade providers that read from different sources
            ITradeDataProvider fileTradeDataProvider = new StreamTradeDataProvider(tradeStream, logger);
            ITradeDataProvider urlTradeDataProvider = new URLTradeDataProvider(tradeURL, logger);
            ITradeDataProvider restfulProvider = new RestfulTradeDataProvider(azureRestfulURL, logger);

            ITradeMapper tradeMapper = new SimpleTradeMapper();
            ITradeParser tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);
            ITradeStorage tradeStorage = new AdoNetTradeStorage(logger);

            TradeProcessor tradeProcessor = new TradeProcessor(restfulProvider, tradeParser, tradeStorage);
            tradeProcessor.ProcessTrades();

            Console.ReadKey();
        }
    }
}
