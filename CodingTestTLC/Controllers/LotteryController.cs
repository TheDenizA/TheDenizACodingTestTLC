using CodingTestTLC.Models;
using CodingTestTLC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestTLC.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LotteryController(LotteryService lotteryService) : ControllerBase
{
    private readonly LotteryService _lotteryService = lotteryService;

    [HttpPost]
    public async Task<LotteryRequestModel> Purchase(LotteryRequestModel request)
    {        
        return await _lotteryService.PurchaseLotteryTicket(request);
    }
}
