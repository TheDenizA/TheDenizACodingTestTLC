using CodingTestTLC.Exceptions;
using CodingTestTLC.Models;
using System.Collections.Concurrent;

namespace CodingTestTLC.Repositories;

public class PurchaseRepo : IPurchaseRepo
{
    // This is already mocked and won't need any further mocking during a test

    // Mock list
    private readonly ConcurrentDictionary<long, LotteryRequestModel> _purchaseRequests;

    public PurchaseRepo()
    {
        _purchaseRequests = new ConcurrentDictionary<long, LotteryRequestModel>();
    }

    // This is a used during testing.
    // I am aware that having testing code mixed in with real code is a big no no, but considering all this is is a mock
    // It makes more sense then to recreate the extact same mock for testing
    public List<LotteryRequestModel> GetPurchaseRequests() => _purchaseRequests.Values.ToList();

    public Task Create(LotteryRequestModel request)
    {
        // This would come from the database as the PK
        // The database would take care of it being unique when being saved
        var id = !_purchaseRequests.Any() ? 1 : _purchaseRequests.Keys.Max() + 1;
        request.AdduniquePurchaseId(id);

        // In this mock case as the db would add it and create a new ID, it would always work.
        _purchaseRequests.TryAdd(id, request);
        
        return Task.CompletedTask;
    }

    public Task Update(LotteryRequestModel request)
    {
        // This would be be one call in a db, just the nature of the mock doing a concurrentDictionary
        // Makes be do 2 calls to get the original data so I can update it

        // Mocks data doesn't exist in a db
        if(!_purchaseRequests.TryGetValue(request.UniquePurchaseId, out var existingRequest))
        {
            throw new NotFoundLotteryRequestException();
        }

        // Mocks data can't be updated in a db
        if (!_purchaseRequests.TryUpdate(request.UniquePurchaseId, request, existingRequest))
        {
            throw new CouldNotUpdateLotteryRequestException();
        }

        return Task.CompletedTask;

    }
}

