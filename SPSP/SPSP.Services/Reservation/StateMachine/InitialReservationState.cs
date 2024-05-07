using AutoMapper;
using Microsoft.AspNetCore.Http;
using SPSP.Models.Request.Reservation;
using SPSP.Services.Database;
using SPSP.Services.Reservation.StateMachine.Generics;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SPSP.Services.Reservation.StateMachine
{
    public class InitialReservationState
        : BaseState
    {

        private readonly IHttpContextAccessor httpContextAccessor;

        public InitialReservationState(IServiceProvider serviceProvider, DataDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(serviceProvider, context, mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override async Task<Models.Reservation> Create(ReservationCreateRequest create)
        {
            var set = context.Set<Database.Reservation>();

            var entity = mapper.Map<Database.Reservation>(create);
            entity.Status = "PENDING_CONFIRMATION";

            var principal = httpContextAccessor.HttpContext?.User;
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            entity.CustomerId = int.Parse(userId!);

            set.Add(entity);

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(entity);
        }

        public override async Task<List<string>> GetAllowedActions()
        {
            var allowedActions = await base.GetAllowedActions();
            allowedActions.AddRange(new List<string> { "create" }); //mogu li se ovdje staviti create i put on hold ili cu u create-u radit provjeru?
            return allowedActions;
        }
    }
}
