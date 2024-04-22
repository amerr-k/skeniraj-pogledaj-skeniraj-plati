using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.Request.SaleInvoiceItem
{
    public class SaleInvoiceItemCreateRequest
    {
        public int? MenuItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
