using SPSP.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SPSP.Models.Request.MoodTracker
{
    public class MoodTrackerUpdateRequest
    {
        [Required]
        public int CustomerId { get; set; }
        public string? DodatniOpis { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DatumEvidencije { get; set; }
        [Required]
        public MoodTrackerStatusEnum MoodTrackerStatusEnum { get; set; }
    }
}