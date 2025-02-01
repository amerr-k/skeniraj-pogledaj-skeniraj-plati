using SPSP.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models
{
    public partial class MoodTracker
    {
        public int Id { get; set; }
        public string? DodatniOpis { get; set; }
        public DateTime DatumEvidencije { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string MoodTrackerStatus { get; set; }
        public bool? Valid { get; set; }

    }
}
