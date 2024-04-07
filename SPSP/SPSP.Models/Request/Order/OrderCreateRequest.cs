using SPSP.Models;
using SPSP.Models.Request.OrderItem;
using SPSP.Models.Request.QRTable;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPSP.Models.Request.Order
{
    public class OrderCreateRequest
    {

        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime OrderDateTime { get; set; }

        public decimal? TotalAmountWithVAT { get; set; }
        public decimal? TotalAmount { get; set; }
        public IEnumerable<OrderItemCreateRequest>? OrderItems { get; set; }
        public int QRTableId { get; set; }
        //mozda ne trebaju stvari ispod
        public decimal? VAT { get; set; }

    }
}