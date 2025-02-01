using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models.Request.MoodTracker;
using SPSP.Models.SearchObjects;
using SPSP.Services.MoodTracker;
using SPSP.Services.Base;

namespace SPSP.Controllers
{
    [ApiController] 
    public class MoodTrackerController
        : BaseCRUDController<Models.MoodTracker, MoodTrackerSearchObject, MoodTrackerCreateRequest, MoodTrackerUpdateRequest>
    {

        public MoodTrackerController(ILogger<BaseCRUDController<Models.MoodTracker, MoodTrackerSearchObject, MoodTrackerCreateRequest, MoodTrackerUpdateRequest>> logger,
            IMoodTrackerService service)
            : base(logger, service)
        {
 
        }

    }
}
