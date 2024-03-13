using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
        public bool? Valid { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
