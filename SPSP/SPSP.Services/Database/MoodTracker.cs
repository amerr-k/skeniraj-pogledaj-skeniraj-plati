using SPSP.Models.Enums;
using System;
using System.Collections.Generic;

#nullable disable

namespace SPSP.Services.Database
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
