using CodingTestTLC.Exceptions;
using CodingTestTLC.Models;
using System.Collections.Concurrent;

namespace CodingTestTLC.Repositories;

public class PurchaseRepo : IPurchaseRepo
{
    // Mock list
    private readonly ConcurrentDictionary<Guid, LotteryRequestModel> _purchaseRequests;

    public PurchaseRepo()
    {
        _purchaseRequests = new ConcurrentDictionary<Guid, LotteryRequestModel>();
    }

    public Task Create(LotteryRequestModel request)
    {
        if(!_purchaseRequests.TryAdd(request.UniquePurchaseID, request))
        {
            throw new DuplicateLotteryRequestException();
        }
        
        return Task.CompletedTask;
    }

    public Task Update(LotteryRequestModel request)
    {
        if(!_purchaseRequests.TryGetValue(request.UniquePurchaseID, out var existingRequest))
        {
            throw new NotFoundLotteryRequestException();
        }

        if (!_purchaseRequests.TryUpdate(request.UniquePurchaseID, request, existingRequest))
        {
            throw new CouldNotUpdateLotteryRequestException();
        }

        return Task.CompletedTask;

    }
}

