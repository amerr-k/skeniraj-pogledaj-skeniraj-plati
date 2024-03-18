using AutoMapper;
using SPSP.Models.Request.Reservation;
using SPSP.Services.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPSP.Services.Reservation.StateMachine
{
    public class InitialReservationState
        : BaseState
    {
        public InitialReservationState(IServiceProvider serviceProvider, DataDbContext context, IMapper mapper)
            : base(serviceProvider, context, mapper)
        {
        }

        public override async Task<Models.Reservation> Create(ReservationCreateRequest create)
        {
            var set = context.Set<Database.Reservation>();

            var entity = mapper.Map<Database.Reservation>(create);
            entity.Status = "PENDING_CONFIRMATION";
            set.Add(entity);

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(entity);
        }

        public override async Task<Models.Reservation> PutReservationOnHold(ReservationCreateRequest create)
        {
            var set = context.Set<Database.Reservation>();

            var entity = mapper.Map<Database.Reservation>(create);
            entity.Status = "ON_HOLD";
            set.Add(entity);

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(entity);
        }

        public override async Task<List<string>> GetAllowedActions()
        {
            var allowedActions = await base.GetAllowedActions();
            allowedActions.AddRange(new List<string> { "Create", "PutOnHold" }); //mogu li se ovdje staviti create i put on hold ili cu u create-u radit provjeru?
            return allowedActions;
        }
    }
}
