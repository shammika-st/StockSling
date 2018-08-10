using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockSlingBusiness.Interfaces;
using StockSlingDTO;

namespace StockSling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IForecastBusiness _forecastBusiness;

        public ForecastController(IForecastBusiness forecastBusiness)
        {
            _forecastBusiness = forecastBusiness;
        }

        [HttpGet]
        [Route("historicaldata")]
        public async Task<dynamic> GetHistoricalStockData()
        {
            var result = await _forecastBusiness.GetHistoricalTradeData();
            return result;
        }
    }
}