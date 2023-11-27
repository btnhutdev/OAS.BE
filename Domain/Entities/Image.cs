using System;

namespace Domain.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public Guid? IdImage { get; set; }

        public string ImageName { get; set; } = null!;

        public byte[]? Data { get; set; }

        public string? Extension { get; set; }

        public Guid? IdProduct { get; set; }

        public string? S3Uri { get; set; }

        public virtual Product? IdProductNavigation { get; set; }
    }
}
