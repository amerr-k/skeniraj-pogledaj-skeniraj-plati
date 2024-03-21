using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class Business
    {
        public Business()
        {
            Employees = new HashSet<Employee>();
            Menus = new HashSet<Menu>();
            QRTables = new HashSet<QRTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public bool? Valid { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<QRTable> QRTables { get; set; }
    }
}
