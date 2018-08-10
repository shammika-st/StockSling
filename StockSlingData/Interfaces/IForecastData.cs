using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using StockSlingDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSlingData.Interfaces
{
    public interface IForecastData
    {
        Task<TableQuerySegment<HistoricalTradesEntity>> GetHistoricalTradeData();
    }
}
