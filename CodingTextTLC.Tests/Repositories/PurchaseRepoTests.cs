using CodingTestTLC.Exceptions;
using CodingTestTLC.Models;
using CodingTestTLC.Repositories;

namespace CodingTextTLC.Tests.Services;

public class PurchaseRepoTests
{
    [Test]
    public async Task ShouldCreatePurchaseRequests()
    {
        IDatabase mockDatabase = new Database();
        IPurchaseRepo repo = new PurchaseRepo(mockDatabase);

        var inputModel1 = new LotteryRequestModel("custID-1", "drawID-1", 1);
        var inputModel2 = new LotteryRequestModel("custID-2", "drawID-2", 2);
        var inputModel3 = new LotteryRequestModel("custID-3", "drawID-3", 3);

        await repo.CreateAsync(inputModel1);
        await repo.CreateAsync(inputModel2);
        await repo.CreateAsync(inputModel3);

        var dbPurchaseRequests = await mockDatabase.GetPurchaseRequestsAsync();

        Assert.That(dbPurchaseRequests.Count, Is.EqualTo(3));

        Assert.That(dbPurchaseRequests[0].CustomerId, Is.EqualTo("custID-1"));
        Assert.That(dbPurchaseRequests[0].DrawId, Is.EqualTo("drawID-1"));
        Assert.That(dbPurchaseRequests[0].NumberOfTickets, Is.EqualTo(1));
        Assert.That(dbPurchaseRequests[0].UniquePurchaseId, Is.EqualTo(1));
        Assert.That(dbPurchaseRequests[0].PurchaseTotal, Is.EqualTo(null));

        Assert.That(dbPurchaseRequests[1].CustomerId, Is.EqualTo("custID-2"));
        Assert.That(dbPurchaseRequests[1].DrawId, Is.EqualTo("drawID-2"));
        Assert.That(dbPurchaseRequests[1].NumberOfTickets, Is.EqualTo(2));
        Assert.That(dbPurchaseRequests[1].UniquePurchaseId, Is.EqualTo(2));
        Assert.That(dbPurchaseRequests[1].PurchaseTotal, Is.EqualTo(null));

        Assert.That(dbPurchaseRequests[2].CustomerId, Is.EqualTo("custID-3"));
        Assert.That(dbPurchaseRequests[2].DrawId, Is.EqualTo("drawID-3"));
        Assert.That(dbPurchaseRequests[2].NumberOfTickets, Is.EqualTo(3));
        Assert.That(dbPurchaseRequests[2].UniquePurchaseId, Is.EqualTo(3));
        Assert.That(dbPurchaseRequests[2].PurchaseTotal, Is.EqualTo(null));
    }

    [Test]
    public async Task ShouldUpdatePurchaseRequest()
    {
        IDatabase mockDatabase = new Database();
        IPurchaseRepo repo = new PurchaseRepo(mockDatabase);

        // Add Test Data
        var inputModel1 = new LotteryRequestModel("custID-1", "drawID-1", 1);
        var inputModel2 = new LotteryRequestModel("custID-2", "drawID-2", 2);
        var inputModel3 = new LotteryRequestModel("custID-3", "drawID-3", 3);

        await repo.CreateAsync(inputModel1);
        await repo.CreateAsync(inputModel2);
        await repo.CreateAsync(inputModel3);

        inputModel2.ConfirmPurchase(45.60m);
        await repo.UpdateAsync(inputModel2);

        var dbPurchaseRequests = await mockDatabase.GetPurchaseRequestsAsync();

        Assert.That(dbPurchaseRequests.Count, Is.EqualTo(3));

        // Ensure Not updated
        Assert.That(dbPurchaseRequests[0].CustomerId, Is.EqualTo("custID-1"));
        Assert.That(dbPurchaseRequests[0].DrawId, Is.EqualTo("drawID-1"));
        Assert.That(dbPurchaseRequests[0].NumberOfTickets, Is.EqualTo(1));
        Assert.That(dbPurchaseRequests[0].UniquePurchaseId, Is.EqualTo(1));
        Assert.That(dbPurchaseRequests[0].PurchaseTotal, Is.EqualTo(null));

        // This should have only purchase total updated
        Assert.That(dbPurchaseRequests[1].CustomerId, Is.EqualTo("custID-2"));
        Assert.That(dbPurchaseRequests[1].DrawId, Is.EqualTo("drawID-2"));
        Assert.That(dbPurchaseRequests[1].NumberOfTickets, Is.EqualTo(2));
        Assert.That(dbPurchaseRequests[1].UniquePurchaseId, Is.EqualTo(2));
        Assert.That(dbPurchaseRequests[1].PurchaseTotal, Is.EqualTo(45.60m));

        // Ensure Not updated
        Assert.That(dbPurchaseRequests[2].CustomerId, Is.EqualTo("custID-3"));
        Assert.That(dbPurchaseRequests[2].DrawId, Is.EqualTo("drawID-3"));
        Assert.That(dbPurchaseRequests[2].NumberOfTickets, Is.EqualTo(3));
        Assert.That(dbPurchaseRequests[2].UniquePurchaseId, Is.EqualTo(3));
        Assert.That(dbPurchaseRequests[2].PurchaseTotal, Is.EqualTo(null));
    }

    [Test]
    public async Task ShouldThrowWhenUpdatePurchaseRequestFailsToFindData()
    {
        IDatabase mockDatabase = new Database();
        IPurchaseRepo repo = new PurchaseRepo(mockDatabase);

        var inputModel1 = new LotteryRequestModel("custID-1", "drawID-1", 1);
        inputModel1.ConfirmPurchase(45.60m);

        Assert.ThrowsAsync<NotFoundLotteryRequestException>(() => repo.UpdateAsync(inputModel1));

        var dbPurchaseRequests = await mockDatabase.GetPurchaseRequestsAsync();

        Assert.That(dbPurchaseRequests.Count, Is.EqualTo(0));
    }
}
