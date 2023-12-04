namespace Core.ViewModel
{
    public class DetailAuctionViewModel
    {
        public int Id { get; set; }

        public Guid? IdDetailAuction { get; set; }

        public Guid? IdAuction { get; set; }

        public Guid? IdBidder { get; set; }

        public double? CurrentPrice { get; set; }

        public double? MaxBidPrice { get; set; }

        public int? AuctionType { get; set; }
    }
}
