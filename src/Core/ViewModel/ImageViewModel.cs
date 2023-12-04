using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class ImageViewModel
    {
        public int Id { get; set; }

        public Guid? IdImage { get; set; }

        public string ImageName { get; set; } = null!;

        public byte[]? Data { get; set; }

        public string? Extension { get; set; }

        public Guid? IdProduct { get; set; }

        public string S3Uri { get; set; }
    }
}
