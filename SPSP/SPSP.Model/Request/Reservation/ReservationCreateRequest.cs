using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.Request.Reservation
{
    public class ReservationCreateRequest
    {
        [Required]
        public int QRTableId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string ContactInfo { get; set; }
        public string? SpecialRequest { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vrijeme početka rezervacije je obavezno.")]
        [DataType(DataType.DateTime)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }
    }
}
