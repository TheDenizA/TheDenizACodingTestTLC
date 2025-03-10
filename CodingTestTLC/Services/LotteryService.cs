using CodingTestTLC.Models;
using CodingTestTLC.Partners;
using CodingTestTLC.Repositories;

namespace CodingTestTLC.Services;

public class LotteryService(IPurchaseRepo purchaseRepo, IThirdPartyService thirdPartyService)
{
    private readonly IPurchaseRepo _purchaseRepo = purchaseRepo;
    private readonly IThirdPartyService _thirdPartyService = thirdPartyService;

    public async Task<decimal> PurchaseLotteryTicket(LotteryRequestModel request)
    {
        await _purchaseRepo.Create(request);

        var lotteryResponse = await _thirdPartyService.RequestPurchase(request);
        request.ConfirmPurchase(lotteryResponse.Total);

        await _purchaseRepo.Update(request);

        return lotteryResponse.Total;
    }
}

