using AutoMapper;
using SPSP.Models.Request.Reservation;
using SPSP.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Services.Reservation.StateMachine
{
    public class OnHoldReservationState
        : BaseState
    {
        public OnHoldReservationState(IServiceProvider serviceProvider, DataDbContext context, IMapper mapper)
            : base(serviceProvider, context, mapper)
        {
        }

        public override async Task<Models.Reservation> CancelReservation(int id)
        {
            var set = context.Set<Database.Reservation>();

            var entity = await set.FindAsync(id);

            entity.Status = "CANCELED";

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(entity);
        }

        public override async Task<Models.Reservation> SwitchToPendingConfirmation(int id)
        {
            var set = context.Set<Database.Reservation>();

            var entity = await set.FindAsync(id);

            entity.Status = "PENDING_CONFIRMATION";

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(entity);
        }

        public override async Task<List<string>> GetAllowedActions()
        {
            var allowedActions = await base.GetAllowedActions();
            allowedActions.AddRange(new List<string> { "CancelReservation" , "SwitchToPendingConfirmation" }); //mogu li se ovdje staviti create i put on hold ili cu u create-u radit provjeru?
            return allowedActions;
        }
    }
}
