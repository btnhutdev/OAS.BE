using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public Guid IdUser { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool? Gender { get; set; }

        public Guid? IdPermission { get; set; }

        public virtual Permission? IdPermissionNavigation { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

        public virtual ICollection<DetailAuction> DetailAuctions { get; set; } = new List<DetailAuction>();

        public virtual ICollection<HistoryPayment> HistoryPayments { get; set; } = new List<HistoryPayment>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
