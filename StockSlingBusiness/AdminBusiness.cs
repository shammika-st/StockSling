using StockSlingBusiness.Interfaces;
using StockSlingData.Interfaces;
using StockSlingDTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockSlingBusiness
{
    public class AdminBusiness : IAdminBusiness
    {
        private readonly IAdminData _adminData;

        public AdminBusiness(IAdminData adminData)
        {
            _adminData = adminData;
        }

        public async Task<string> GetCurrentStock()
        {
            var stocks = await _adminData.GetCurrentStock();

            return stocks.Results.ToList().OrderByDescending(st => st.Timestamp).FirstOrDefault().RowKey;
        }

        public void SetCurrentStock(NewStockDTO newStockDTO)
        {
            _adminData.SetCurrentStock(newStockDTO);
        }
    }
}
