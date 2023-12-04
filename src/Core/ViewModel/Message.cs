namespace Core.ViewModel
{
    public class Message
    {
        public Guid? AuctionId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? BidderId { get; set; }
        public float CurrentPrice { get; set; }
        public float MyPrice { get; set; }
        public int AuctionType { get; set; }
        public float MyMaxPriceAuto { get; set; }
        public float StepPrice { get; set; }
    }
}
