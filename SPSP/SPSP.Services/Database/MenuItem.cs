using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int MenuId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Valid { get; set; }
        public virtual Category Category { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
