using System.ComponentModel.DataAnnotations;

namespace CodingTestTLC.Models;

public class LotteryRequestModel
{
    public Guid UniquePurchaseID { get; private set; }

    [Required]
    public string CustomerId { get; private set; }

    [Required]
    public string DrawId { get; private set; }

    [Range(1, int.MaxValue)]
    public int NumberOfTickets { get; private set; }

    public DateTime Timestamp { get; private set; }

    public decimal? PurchaseTotal { get; private set; }

    public LotteryRequestModel(string customerId, string drawId, int numberOfTickets)
    {
        UniquePurchaseID = Guid.NewGuid();
        CustomerId = customerId;
        DrawId = drawId;
        NumberOfTickets = numberOfTickets;
        Timestamp = DateTime.UtcNow;
    }

    public void ConfirmPurchase(decimal total)
    {
        PurchaseTotal = total;
    }
}

