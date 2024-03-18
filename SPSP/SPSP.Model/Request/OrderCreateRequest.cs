using System;

namespace SPSP.Services
{
    public class OrderCreateRequest
    {

        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}