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
    public class AdminData : IAdminData
    {
        public async Task<TableQuerySegment<StocksEntity>> GetCurrentStock()
        {
            var table = DBHelpers.GetTableReference("Stock");

            // Construct the query operation for all customer entities where PartitionKey="dislike".
            TableQuery<StocksEntity> query = new TableQuery<StocksEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "stock"));
            TableContinuationToken token = null;
            TableQuerySegment<StocksEntity> segment = null;

            do
            {
                segment = await table.Result.ExecuteQuerySegmentedAsync(query, token);

                // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                token = segment.ContinuationToken;
            }
            while (token != null);

            return segment;
        }

        public void SetCurrentStock(NewStockDTO newStockDTO)
        {
            var table = DBHelpers.GetTableReference("Stock");

            // Create a new stock entity.
            StocksEntity stock = new StocksEntity(newStockDTO.StockName);
            stock.ChangedBy = newStockDTO.Password;

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(stock);

            // Execute the insert operation.
            table.Result.ExecuteAsync(insertOperation);
        }

        #region Private Methods
        
        #endregion
    }
}
