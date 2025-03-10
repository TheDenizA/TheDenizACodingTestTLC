using CodingTestTLC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestTLC.Controllers;

[ApiController]
[Route("[controller]")]
public class LotteryController : ControllerBase
{
    private readonly ILogger<LotteryController> _logger;

    public LotteryController(ILogger<LotteryController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "RequestLotteryTicket")]
    public LotteryRequestModel Get()
    {
        return new LotteryRequestModel
        {

        };
    }
}
