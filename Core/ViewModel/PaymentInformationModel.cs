namespace Core.ViewModel
{
    public class PaymentInformationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ZIPCode { get; set; }
        public string Telephone { get; set; }
        public string ShipingAddress { get; set; }
        public string OrderNotes { get; set; }
        public double? TotalPrice { get; set; }
        public string OrderType { get; set; }
    }
}
