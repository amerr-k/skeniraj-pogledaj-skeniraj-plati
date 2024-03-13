﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models
{
    public class PagedResult<T> 
    {
        public List<T> Result { get; set; }
        public int? Count { get; set; }
    }
}
