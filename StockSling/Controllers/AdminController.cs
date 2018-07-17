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
    public class AdminController : ControllerBase
    {
        private readonly IAdminBusiness _adminBusiness;

        public AdminController(IAdminBusiness adminBusiness)
        {
            _adminBusiness = adminBusiness;
        }

        [HttpGet]
        [Route("stock")]
        public async Task<dynamic> GetCurrentStock()
        {
            var result = await _adminBusiness.GetCurrentStock();
            return new { StockName = result };
        }

        [HttpPost]
        [Route("SetStock")]
        public void SetCurrentStock(NewStockDTO newStockDTO)
        {
            if (!newStockDTO.Password.ToUpper().Equals("NIKHIL"))
                throw new Exception("Invalid Password!");
            else
            {
                _adminBusiness.SetCurrentStock(newStockDTO);
            }
        }
    }
}