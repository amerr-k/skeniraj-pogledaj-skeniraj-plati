using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.Request.Reservation
{
    public class ReservationCreateRequest
    {
        public int QRTableId { get; set; }
        public int CustomerId { get; set; }
        public string ContactInfo { get; set; }
        public string? SpecialRequest { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        //public string? Status { get; set; }
    }
}
