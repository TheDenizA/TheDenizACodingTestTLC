using CodingTestTLC.Models;

namespace CodingTestTLC.Partners;


public interface IThirdPartyService
{
    Task<LotteryResponseModel> RequestPurchase(LotteryRequestModel request);
}

