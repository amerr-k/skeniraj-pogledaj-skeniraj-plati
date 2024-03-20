using System;
using System.ComponentModel.DataAnnotations;

namespace SPSP.Services
{
    public class OrderCreateRequest
    {

        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}