using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSlingBusiness.Interfaces
{
    public interface IForecastBusiness
    {
        Task<List<HistoricalTradesEntity>> GetHistoricalTradeData();
    }
}
