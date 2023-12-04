using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class MailModel
    {
        public Guid? IdBidder { get; set; }

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double PriceCurrentMax { get; set; }

        public double? CurrentPrice { get; set; }

        public string AuctionType { get; set; }

        public string Email { get; set; }

        public double Init_Price { get; set; }

        public double Step_Price { get; set; }
    }
}
