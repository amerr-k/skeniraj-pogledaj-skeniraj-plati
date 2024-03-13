using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public bool IsAdmin { get; set; }
        public bool? Valid { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
