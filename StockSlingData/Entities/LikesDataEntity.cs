using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSlingData.Entities
{
    public class LikesDataEntity : TableEntity
    {
        public LikesDataEntity(string actionName)
        {
            this.PartitionKey = actionName;
            this.RowKey = actionName;
        }

        public LikesDataEntity() { }

        public int Count { get; set; }

        public string ipAddress { get; set; }
    }
}
