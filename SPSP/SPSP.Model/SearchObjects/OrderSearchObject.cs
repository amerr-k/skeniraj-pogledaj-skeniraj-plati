﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.SearchObjects
{
    public class OrderSearchObject : BaseSearchObject
    {
        public bool? IsOrderItemsIncluded { get; set; }
    }
}
