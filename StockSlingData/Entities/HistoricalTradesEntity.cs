using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSlingData.Entities
{
    public class HistoricalTradesEntity : TableEntity
    {
        public HistoricalTradesEntity(string stockName)
        {
            this.PartitionKey = "Partition1";
            this.RowKey = stockName;
        }

        public HistoricalTradesEntity() { }

        public string StockName { get; set; }

        public double IV { get; set; }

        public bool IsCreditType { get; set; }

        public double TypeValue { get; set; }
    }
}
