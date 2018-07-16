using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockSlingBusiness.Interfaces;

namespace StockSling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {
        private readonly ICounterBusiness _counterBusiness;

        public CounterController(ICounterBusiness counterBusiness)
        {
            _counterBusiness = counterBusiness;
        }

        [HttpGet]
        [Route("like")]
        public async Task<int> GetLikeCount()
        {
            return await _counterBusiness.GetLikeCount();
        }

        [HttpGet]
        [Route("dislike")]
        public async Task<int> GetDislikeCount()
        {
            return await _counterBusiness.GetDislikeCount();
        }

        [HttpPost]
        [Route("{actionName}")]
        public int UpdateCount(string actionName)
        {
            return _counterBusiness.UpdateCount(actionName);
        }
    }
}