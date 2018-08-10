using StockSlingData.Interfaces;
using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for StorageAccounts
using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;
using StockSlingDTO;
using StockSlingData.Helper;

namespace StockSlingData
{
    public class ForecastData : IForecastData
    {
        public async Task<TableQuerySegment<HistoricalTradesEntity>> GetHistoricalTradeData()
        {
            var table = DBHelpers.GetTableReference("HistoricalTrades");

            // Construct the query operation for all customer entities where PartitionKey=actionName.
            TableQuery<HistoricalTradesEntity> query = new TableQuery<HistoricalTradesEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Partition1"));
            TableContinuationToken token = null;
            TableQuerySegment<HistoricalTradesEntity> segment = null;

            do
            {
                segment = await table.Result.ExecuteQuerySegmentedAsync(query, token);

                // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                token = segment.ContinuationToken;
            }
            while (token != null);

            return segment;
        }
    }
}
