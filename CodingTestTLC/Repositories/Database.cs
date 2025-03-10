using CodingTestTLC.Exceptions;
using CodingTestTLC.Models;
using System.Collections.Concurrent;

namespace CodingTestTLC.Repositories
{
    public class Database : IDatabase
    {
        // Mock list
        private readonly ConcurrentDictionary<long, LotteryRequestModel> _purchaseRequests;

        public Database()
        {
            _purchaseRequests = new ConcurrentDictionary<long, LotteryRequestModel>();
        }

        public Task<List<LotteryRequestModel>> GetPurchaseRequestsAsync() => Task.FromResult(_purchaseRequests.Values.ToList());

        public Task CreateAsync(LotteryRequestModel request)
        {
            // This would come from the database as the PK
            // The database would take care of it being unique when being saved
            var id = !_purchaseRequests.Any() ? 1 : _purchaseRequests.Keys.Max() + 1;
            request.AddUniquePurchaseId(id);

            // In this mock case as the db would add it and create a new ID, it would always work.
            _purchaseRequests.TryAdd(id, request);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(LotteryRequestModel request)
        {
            // Mocks data doesn't exist in a db
            if (!_purchaseRequests.TryGetValue(request.UniquePurchaseId, out var existingRequest))
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
}
