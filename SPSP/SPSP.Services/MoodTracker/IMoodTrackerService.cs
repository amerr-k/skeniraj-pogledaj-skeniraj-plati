using SPSP.Models.Request.MoodTracker;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.MoodTracker
{
    public interface IMoodTrackerService
        : ICRUDService<Models.MoodTracker, MoodTrackerSearchObject, MoodTrackerCreateRequest, MoodTrackerUpdateRequest>
    {

    }
}
