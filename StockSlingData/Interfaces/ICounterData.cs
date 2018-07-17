using Microsoft.WindowsAzure.Storage.Table;
using StockSlingData.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSlingData.Interfaces
{
    public interface ICounterData
    {
        Task<TableQuerySegment<LikesDataEntity>> GetLikesCountByAction(string actionName);

        Task<int> UpdateCount(string actionName);
    }
}
