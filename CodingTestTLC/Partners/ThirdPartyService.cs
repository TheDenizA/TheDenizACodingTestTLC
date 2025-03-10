using CodingTestTLC.Models;

namespace CodingTestTLC.Partners;

public class ThirdPartyService : IThirdPartyService
{
    public Task<LotteryResponseModel> RequestPurchase(LotteryRequestModel request)
    {
        var rnd = new Random();
        return Task.FromResult(new LotteryResponseModel((decimal)rnd.Next(10, 1000)));
    }
}
