using Microsoft.AspNetCore.Http;

namespace Core.ViewModel
{
    public class DetailProductBidderViewModel
    {
        public Guid? IdProduct { get; set; }

        public Guid? IdAuction { get; set; }

        public string ProductName { get; set; }

        public double InitPrice { get; set; }

        public double StepPrice { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsApprove { get; set; }

        public bool? IsSold { get; set; }

        public bool? IsPayment { get; set; }

        public Guid? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public Guid? UserId { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }

        public List<ImageViewModel>? Images { get; set; }

        public bool? IsReject { get; set; }

        public double Duration { get; set; }

        public bool? IsStart { get; set; }

        public bool? IsEnd { get; set; }

        public bool? HasBid { get; set; }

        public double PriceCurrentMax { get; set; }

        public string AuctionType { get; set; }

    }
}
