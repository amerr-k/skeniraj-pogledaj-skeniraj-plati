using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class Business
    {
        public Business()
        {
            Menus = new HashSet<Menu>();
            QRTables = new HashSet<QRTable>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }
        public bool? Valid { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<QRTable> QRTables { get; set; }
    }
}
