using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models;
using SPSP.Models.Request.Reservation;
using SPSP.Models.SearchObjects;
using SPSP.Services.Reservation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPSP.Controllers
{
    [ApiController]
    public class ReservationController 
        : BaseCRUDController<Models.Reservation, BaseSearchObject, ReservationCreateRequest, ReservationUpdateRequest>
    {
        protected new readonly IReservationService service;

        public ReservationController(ILogger<BaseCRUDController<Reservation, BaseSearchObject, ReservationCreateRequest, ReservationUpdateRequest>> logger,
            IReservationService service)
            : base(logger, service)
        {
            this.service = service;
        }

        [HttpPut("{id}/confirm")]
        public virtual async Task<Models.Reservation> ConfirmReservation(int id)
        {
            
            return await service.ConfirmReservation(id);
            //return await (service as IReservationService).Confirm(id);
            //moze i ovako
        }

        [HttpPut("{id}/cancel")]
        public virtual async Task<Models.Reservation> CancelReservation(int id)
        {

            return await service.CancelReservation(id);
        }

        [HttpPost("onHold")]
        public virtual async Task<Models.Reservation> PutReservationOnHold(ReservationCreateRequest create)
        {
            return await service.PutReservationOnHold(create);
        }

        [HttpPost("pendingConfirmation")]
        public virtual async Task<Models.Reservation> SwitchToPendingConfirmation(int id)
        {
            return await service.SwitchToPendingConfirmation(id);
        }

        [HttpGet("{id}/allowedActions")]
        public virtual async Task<List<string>> GetAllowedActions(int id)
        {
            return await service.GetAllowedActions(id);
        }

    }
}
