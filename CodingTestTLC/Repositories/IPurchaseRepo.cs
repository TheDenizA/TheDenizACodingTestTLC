using CodingTestTLC.Models;

namespace CodingTestTLC.Repositories;

public interface IPurchaseRepo
{
    Task CreateAsync(LotteryRequestModel request);
    Task UpdateAsync(LotteryRequestModel request);
}

