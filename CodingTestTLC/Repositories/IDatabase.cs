using CodingTestTLC.Models;

namespace CodingTestTLC.Repositories
{
    public interface IDatabase
    {
        List<LotteryRequestModel> GetPurchaseRequests();
        Task CreateAsync(LotteryRequestModel request);
        Task UpdateAsync(LotteryRequestModel request);
    }
}
