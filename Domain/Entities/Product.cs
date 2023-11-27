using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public Guid? IdProduct { get; set; }

        public string ProductName { get; set; } = null!;

        public double InitPrice { get; set; }

        public double StepPrice { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsApprove { get; set; }

        public bool? IsSold { get; set; }

        public bool? IsPayment { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? UserId { get; set; }

        public bool? IsReject { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

        public virtual ICollection<HistoryPayment> HistoryPayments { get; set; } = new List<HistoryPayment>();

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();

        public virtual User? User { get; set; }
    }
}
