using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.ViewModel
{
    public class AuctionViewModel
    {
        public int Id { get; set; }

        public Guid? IdAuction { get; set; }

        public Guid? IdProduct { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double PriceCurrentMax { get; set; }

        public double Duration { get; set; }

        public bool? IsStart { get; set; }

        public bool? IsEnd { get; set; }

        public bool? HasBid { get; set; }

        public Guid? IdWinner { get; set; }
    }
}
