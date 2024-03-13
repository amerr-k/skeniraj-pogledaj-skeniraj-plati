using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Models
{
    public partial class QRTable
    {
        public int Id { get; set; }
        public string QRCode { get; set; }
        public int? TableNumber { get; set; }
        public bool? Valid { get; set; }
    }
}
