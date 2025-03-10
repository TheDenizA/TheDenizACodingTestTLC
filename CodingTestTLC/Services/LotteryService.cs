using CodingTestTLC.Models;
using CodingTestTLC.Partners;
using CodingTestTLC.Repositories;

namespace CodingTestTLC.Services;

public class LotteryService(IPurchaseRepo purchaseRepo, IThirdPartyService thirdPartyService)
{
    private readonly IPurchaseRepo _purchaseRepo = purchaseRepo;
    private readonly IThirdPartyService _thirdPartyService = thirdPartyService;
    
    public async Task<LotteryRequestModel> PurchaseLotteryTicketAsync(LotteryRequestModel request)
    {
        // Create the request, with a unique ID, this saves request data and gives us a record of the request
        await _purchaseRepo.CreateAsync(request);

        // Purchase from third party
        var lotteryResponse = await _thirdPartyService.RequestPurchaseAsync(request);
        request.ConfirmPurchase(lotteryResponse.Total);

        // Update the data with the purchase
        await _purchaseRepo.UpdateAsync(request);

        // Just returing request for visability during this code test
        return request;
    }
}

