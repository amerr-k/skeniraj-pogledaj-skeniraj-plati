using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class Menu
    {
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public bool? Valid { get; set; }

        public virtual Business Business { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
