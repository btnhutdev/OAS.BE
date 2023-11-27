using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public Guid? IdCategory { get; set; }

        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
