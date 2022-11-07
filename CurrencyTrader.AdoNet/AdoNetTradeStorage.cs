using System.Collections.Generic;
using System.Linq;

using CurrencyTrader.Contracts;

namespace CurrencyTrader.AdoNet
{
    public class AdoNetTradeStorage : ITradeStorage
    {
        private readonly ILogger logger;

        public AdoNetTradeStorage(ILogger logger)
        {
            this.logger = logger;
        }

        public void StoreTrades(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Connecting to Database");
            // This users the Azure connection string
            // Remember to include reference to NuGet package System.Data.SqlClient
            string azureConnectString = @"Data Source=cis3115-server.database.windows.net;Initial Catalog=CIS3115;User ID=cis3115;Password=Saints4SQL;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (var connection = new System.Data.SqlClient.SqlConnection(azureConnectString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in trades)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.insert_trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);
                        logger.LogInfo("Adding trade to database...");

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                connection.Close();
            }

            logger.LogInfo("{0} trades processed", trades.Count());
        }
    }
}
