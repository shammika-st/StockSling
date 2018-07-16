using StockSlingBusiness.Interfaces;
using StockSlingData.Interfaces;
using System;
using System.Threading.Tasks;

namespace StockSlingBusiness
{
    public class CounterBusiness : ICounterBusiness
    {
        private readonly ICounterData _counterData;

        public CounterBusiness(ICounterData counterData)
        {
            _counterData = counterData;
        }

        public async Task<int> GetDislikeCount()
        {
            var counts = await _counterData.GetLikesCountByAction("dislike");

            return counts.Results[0].Count;
        }

        public async Task<int> GetLikeCount()
        {
            var counts = await _counterData.GetLikesCountByAction("like");

            return counts.Results[0].Count;
        }

        public int UpdateCount(string actionName)
        {
            return _counterData.UpdateCount(actionName);
        }
    }
}
