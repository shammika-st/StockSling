using StockSlingDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSlingBusiness.Interfaces
{
    public interface IAdminBusiness
    {
        Task<string> GetCurrentStock();

        void SetCurrentStock(NewStockDTO newStockDTO);
    }
}
