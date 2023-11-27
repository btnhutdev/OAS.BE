namespace Core.ViewModel
{
    public class HistoryPaymentViewModel
    {
        public int Id { get; set; }

        public Guid? IdHistoryPayment { get; set; }

        public Guid? IdUser { get; set; }

        public Guid? IdProduct { get; set; }

        public string? OrderType { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? ZipCode { get; set; }

        public string? Telephone { get; set; }

        public string? ShipingAddress { get; set; }

        public string? OrderNotes { get; set; }

        public double? TotalPrice { get; set; }

        public DateTime? DatePayment { get; set; }

        public bool? Status { get; set; }
    }
}
