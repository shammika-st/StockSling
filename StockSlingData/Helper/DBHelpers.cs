using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSlingData.Helper
{
    public static class DBHelpers
    {
        public static async Task<CloudTable> GetTableReference(string tableName)
        {
            CloudTableClient tableClient = GetTableClientReference();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(tableName);

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            return table;
        }

        public static CloudTableClient GetTableClientReference()
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
    }
}
