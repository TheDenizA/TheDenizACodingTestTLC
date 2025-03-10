using CodingTestTLC.Models;

namespace CodingTestTLC.Partners;

public class ThirdPartyService : IThirdPartyService
{
    // This is a mock, of an api returning a total
    // Since the value returned is from a 3rd party I am returing just a random numbere
    public Task<LotteryResponseModel> RequestPurchaseAsync(LotteryRequestModel request)
    {
        var rnd = new Random();
        return Task.FromResult(new LotteryResponseModel((decimal)rnd.Next(10, 1000)));
    }
}
