using SPSP.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.SearchObjects
{
    public class MoodTrackerSearchObject : BaseSearchObject
    {
        public DateTime? DatumEvidencijeSearch { get; set; }
        public int? CustomerId { get; set; }
        public MoodTrackerStatusEnum? MoodTrackerStatusEnum { get; set; }

    }
}
