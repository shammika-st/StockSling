using StockSlingData.Interfaces;
using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for StorageAccounts
using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;
using StockSlingData.Helper;

namespace StockSlingData
{
    public class CounterData : ICounterData
    {
        public async Task<TableQuerySegment<LikesDataEntity>> GetLikesCountByAction(string actionName)
        {
            var table = DBHelpers.GetTableReference("LikesData");

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
            var table = DBHelpers.GetTableReference("LikesData");

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
    }
}
