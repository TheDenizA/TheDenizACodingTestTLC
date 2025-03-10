using CodingTestTLC.Exceptions;
using CodingTestTLC.Models;
using System.Collections.Concurrent;

namespace CodingTestTLC.Repositories;

public class PurchaseRepo(IDatabase database) : IPurchaseRepo
{
    public Task CreateAsync(LotteryRequestModel request) => database.CreateAsync(request);

    public Task UpdateAsync(LotteryRequestModel request) => database.UpdateAsync(request);
}

