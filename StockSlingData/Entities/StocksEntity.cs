using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSlingData.Entities
{
    public class StocksEntity : TableEntity
    {
        public StocksEntity(string stockName)
        {
            this.PartitionKey = "stock";
            this.RowKey = stockName;
        }

        public StocksEntity() { }

        public string ChangedBy { get; set; }
    }
}
