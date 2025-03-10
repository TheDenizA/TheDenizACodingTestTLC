using CodingTestTLC.Models;

namespace CodingTestTLC.Repositories
{
    public interface IDatabase
    {
        Task<List<LotteryRequestModel>> GetPurchaseRequestsAsync();
        Task CreateAsync(LotteryRequestModel request);
        Task UpdateAsync(LotteryRequestModel request);
    }
}
