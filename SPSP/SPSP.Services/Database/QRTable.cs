using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Services.Database
{
    public partial class QRTable
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string QRCode { get; set; }
        public int? TableNumber { get; set; }
        public int? Capacity { get; set; }
        public string? LocationDescription { get; set; }
        public bool IsTaken { get; set; }
        public bool? Valid { get; set; }
        public virtual Business Business { get; set; }
    }
}
