﻿using System;
using System.Collections.Generic;

namespace SPSP.Services.Database
{
    public partial class Category
    {
        public Category()
        {
            MenuItems = new HashSet<MenuItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Valid { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
