using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class PaymentViewModel
    {
        public Guid? IdUser { get; set; }
        public Guid? IdProduct { get; set; }
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
