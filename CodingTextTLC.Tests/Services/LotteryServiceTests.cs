using CodingTestTLC.Models;
using CodingTestTLC.Partners;
using CodingTestTLC.Repositories;
using CodingTestTLC.Services;
using Moq;

namespace CodingTextTLC.Tests.Services;

public class LotteryServiceTests
{
    private Mock<IPurchaseRepo> _mockPurchaseRepo;
    private Mock<IThirdPartyService> _mockThirdPartyService;

    [SetUp]
    public void SetUp()
    {
        _mockPurchaseRepo = new Mock<IPurchaseRepo>();
        _mockThirdPartyService = new Mock<IThirdPartyService>();
    }

    [Test]
    public async Task ShouldPurchaseLotteryTicket()
    {
        var createModel = new LotteryRequestModel("custID-1", "drawID-1", 5);
        createModel.AddUniquePurchaseId(7);
        _mockPurchaseRepo.Setup(x => x.CreateAsync(createModel)).Returns(Task.CompletedTask);

        _mockThirdPartyService.Setup(x => x.RequestPurchaseAsync(createModel)).Returns(Task.FromResult(new LotteryResponseModel(100m)));

        var updateModel = new LotteryRequestModel("custID-1", "drawID-1", 5);
        updateModel.AddUniquePurchaseId(7);
        updateModel.ConfirmPurchase(100m);
        _mockPurchaseRepo.Setup(x => x.UpdateAsync(updateModel)).Returns(Task.CompletedTask);

        var lotteryService = new LotteryService(_mockPurchaseRepo.Object, _mockThirdPartyService.Object);

        var purchasedTicket = await lotteryService.PurchaseLotteryTicketAsync(createModel);

        Assert.That(purchasedTicket.CustomerId, Is.EqualTo("custID-1"));
        Assert.That(purchasedTicket.DrawId, Is.EqualTo("drawID-1"));
        Assert.That(purchasedTicket.NumberOfTickets, Is.EqualTo(5));
        Assert.That(purchasedTicket.UniquePurchaseId, Is.EqualTo(7));
        Assert.That(purchasedTicket.PurchaseTotal, Is.EqualTo(100m));
    }
}
