using CodingTestTLC.Models;

namespace CodingTestTLC.Repositories;

public interface IPurchaseRepo
{
    Task Create(LotteryRequestModel request);
    Task Update(LotteryRequestModel request);
}

