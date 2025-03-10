namespace CodingTestTLC.Models
{
    public class LotteryRequestModel
    {
        public int CustomerId { get; set; }

        public int DrawId { get; set; }

        public int NumberOfTickets { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
