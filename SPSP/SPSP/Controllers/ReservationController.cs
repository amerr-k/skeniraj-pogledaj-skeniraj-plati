using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models;
using SPSP.Models.SearchObjects;
using SPSP.Services.Reservation;

namespace SPSP.Controllers
{
    [ApiController]
    public class ReservationController : BaseController<Models.Reservation, BaseSearchObject>
    {

        public ReservationController(ILogger<BaseController<Reservation, BaseSearchObject>> logger,
            IReservationService service)
            : base(logger, service)
        {
 
        }

    }
}
