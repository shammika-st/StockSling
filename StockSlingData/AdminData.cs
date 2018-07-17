using StockSlingData.Interfaces;
using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for StorageAccounts
using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;
using StockSlingDTO;

namespace StockSlingData
{
    public class AdminData : IAdminData
    {
        public async Task<TableQuerySegment<StocksEntity>> GetCurrentStock()
        {
            var table = GetStockTableReference();

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
            var table = GetStockTableReference();

            // Create a new stock entity.
            StocksEntity stock = new StocksEntity(newStockDTO.StockName);
            stock.ChangedBy = newStockDTO.Password;

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(stock);

            // Execute the insert operation.
            table.Result.ExecuteAsync(insertOperation);
        }

        #region Private Methods
        private static async Task<CloudTable> GetLikesDataTableReference()
        {
            CloudTableClient tableClient = GetTableClientReference();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("LikesData");

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            return table;
        }

        private static async Task<CloudTable> GetStockTableReference()
        {
            CloudTableClient tableClient = GetTableClientReference();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("Stock");

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            return table;
        }

        private static CloudTableClient GetTableClientReference()
        {
            var storageCredentials = new StorageCredentials("stockslingtest", "oVmIDBsxTCWZHjUOOARcqTvTchQdUQUu72BrE4F1ppv39V249grnnu0HlirRdC1uAVdBobLxdFJbntFfn1yfUA==");

            // Parse the connection string and return a reference to the storage account.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //    CloudConfigurationManager.GetSetting("SSAzureTableDbContext"));
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            return tableClient;
        }
        #endregion
    }
}
