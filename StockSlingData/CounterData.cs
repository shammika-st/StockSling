using StockSlingData.Interfaces;
using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for StorageAccounts
using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;

namespace StockSlingData
{
    public class CounterData : ICounterData
    {
        public async Task<TableQuerySegment<LikesDataEntity>> GetLikesCountByAction(string actionName)
        {
            var table = GetLikesDataTableReference();

            // Construct the query operation for all customer entities where PartitionKey=actionName.
            TableQuery<LikesDataEntity> query = new TableQuery<LikesDataEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, actionName));
            TableContinuationToken token = null;
            TableQuerySegment<LikesDataEntity> segment = null;

            do
            {
                segment = await table.Result.ExecuteQuerySegmentedAsync(query, token);

                // Save the continuation token for the next call to ExecuteQuerySegmentedAsync
                token = segment.ContinuationToken;
            }
            while (token != null);

            return segment;
        }

        public async Task<int> UpdateCount(string actionName)
        {
            var table = GetLikesDataTableReference();

            // Create a retrieve operation that takes a LikesDataEntity.
            TableOperation retrieveOperation = TableOperation.Retrieve<LikesDataEntity>(actionName, actionName);

            // Execute the operation.
            TableResult retrievedResult = await table.Result.ExecuteAsync(retrieveOperation);

            // Assign the result to a LikesDataEntity object.
            LikesDataEntity updateEntity = (LikesDataEntity)retrievedResult.Result;

            if (updateEntity != null)
            {
                // Change the phone number.
                updateEntity.Count = updateEntity.Count + 1;

                // Create the Replace TableOperation.
                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                // Execute the operation.
                await table.Result.ExecuteAsync(updateOperation);

                return updateEntity.Count;
            }
            else
            {
                throw new Exception("Entity could not be retrieved.");
            }
        }

        #region Private Methods
        private static async Task<CloudTable> GetLikesDataTableReference()
        {
            var storageCredentials = new StorageCredentials("stockslingtest", "oVmIDBsxTCWZHjUOOARcqTvTchQdUQUu72BrE4F1ppv39V249grnnu0HlirRdC1uAVdBobLxdFJbntFfn1yfUA==");

            // Parse the connection string and return a reference to the storage account.
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            //    CloudConfigurationManager.GetSetting("SSAzureTableDbContext"));
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("LikesData");

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            return table;
        }
        #endregion
    }
}
