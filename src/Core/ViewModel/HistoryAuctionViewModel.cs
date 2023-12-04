namespace Core.ViewModel
{
    public class HistoryAuctionViewModel
    {
        public Guid? IdDetailAuction { get; set; }

        public Guid? IdAuction { get; set; }

        public Guid? IdBidder { get; set; }

        public string BidderName { get; set; }

        public double? CurrentPrice { get; set; }

        public double? MaxBidPrice { get; set; }

        public int? AuctionType { get; set; }

        public string AuctionTypeName { get; set; }
    }
}
