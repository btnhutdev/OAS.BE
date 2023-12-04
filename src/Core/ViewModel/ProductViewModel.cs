using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public Guid? IdProduct { get; set; }

        public string ProductName { get; set; }

        public double InitPrice { get; set; }

        public double StepPrice { get; set; }

        public string? Description { get; set; }

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

        public bool? IsReplaceSearch { get; set; }
    }
}
