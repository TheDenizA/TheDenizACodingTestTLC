using CodingTestTLC.Models;

namespace CodingTestTLC.Partners;


public interface IThirdPartyService
{
    Task<LotteryResponseModel> RequestPurchaseAsync(LotteryRequestModel request);
}

