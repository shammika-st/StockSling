using Microsoft.WindowsAzure.Storage.Table;
using StockSlingBusiness.Interfaces;
using StockSlingData.Entities;
using StockSlingData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockSlingBusiness
{
    public class ForecastBusiness : IForecastBusiness
    {
        private readonly IForecastData _forecastData;

        public ForecastBusiness(IForecastData forecastData)
        {
            _forecastData = forecastData;
        }
        
        public async Task<List<HistoricalTradesEntity>> GetHistoricalTradeData()
        {
            var data = await _forecastData.GetHistoricalTradeData();

            return data.Results.ToList();
        }
    }
}
