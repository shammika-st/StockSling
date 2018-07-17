using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using StockSlingDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSlingData.Interfaces
{
    public interface IAdminData
    {
        Task<TableQuerySegment<StocksEntity>> GetCurrentStock();

        void SetCurrentStock(NewStockDTO newStockDTO);
    }
}
