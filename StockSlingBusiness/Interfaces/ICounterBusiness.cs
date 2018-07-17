using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockSlingBusiness.Interfaces
{
    public interface ICounterBusiness
    {
        Task<int> GetDislikeCount();

        Task<int> GetLikeCount();

        Task<int> UpdateCount(string actionName);
    }
}
