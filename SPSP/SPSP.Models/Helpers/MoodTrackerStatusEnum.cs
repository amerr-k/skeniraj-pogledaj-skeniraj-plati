using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.Enums
{
    public enum MoodTrackerStatusEnum
    {
        HAPPY,
        SAD,
        STRESSED,
        EXCITED,
        TIRED,
    }

    public static class MoodTrackerStatusEnumExtension
    {
        public static string GetValue(this MoodTrackerStatusEnum status)
        {
            switch (status)
            {
                case MoodTrackerStatusEnum.HAPPY:
                    return "Sretan";
                case MoodTrackerStatusEnum.SAD:
                    return "Tužan";
                case MoodTrackerStatusEnum.STRESSED:
                    return "Pod stresom";
                case MoodTrackerStatusEnum.EXCITED:
                    return "Uzbuđen";
                case MoodTrackerStatusEnum.TIRED:
                    return "Umoran";
                default:
                    throw new ArgumentException("Invalid MoodTrackerStatus");
            }
        }
    }
}
