using SPSP.Models;
using SPSP.Models.Request.QRTable;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPSP.Models.Request.OrderItem
{
    public class OrderItemCreateRequest
    {
        public int? MenuItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Subtotal { get; set; }
        public bool? Valid { get; set; }


    }
}