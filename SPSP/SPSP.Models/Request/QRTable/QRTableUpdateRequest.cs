using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.Request.QRTable
{
    public class QRTableUpdateRequest
    {
        public string QRCode { get; set; }
        public int? TableNumber { get; set; }
        public bool? Valid { get; set; }
    }
}
