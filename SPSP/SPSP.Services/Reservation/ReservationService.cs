using AutoMapper;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;
using SPSP.Models.Request.Reservation;
using System.Threading.Tasks;
using SPSP.Services.Reservation.StateMachine;
using System.Collections.Generic;

namespace SPSP.Services.Reservation
{
    public class ReservationService : BaseCRUDService<Models.Reservation, Database.Reservation, BaseSearchObject, ReservationCreateRequest, ReservationUpdateRequest>, IReservationService
    {
        public BaseState baseState { get; set; }
        public ReservationService(BaseState baseState, DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            this.baseState = baseState;
        }

        public override async Task<Models.Reservation> Create(ReservationCreateRequest create)
        {
            var state = baseState.CreateState("INITIAL");

            return await state.Create(create);
        }

        public async Task<Models.Reservation> PutReservationOnHold(ReservationCreateRequest create)
        {
            var state = baseState.CreateState("INITIAL");

            return await state.PutReservationOnHold(create);
        }

        public override async Task<Models.Reservation> Update(int id, ReservationUpdateRequest update)
        {
            var dbEntity = await context.Reservations.FindAsync(id);

            var state = baseState.CreateState(dbEntity?.Status);

            return await state.Update(dbEntity, update);
        }

        public async Task<Models.Reservation> ConfirmReservation(int id)
        {
            var entity = await context.Reservations.FindAsync(id);

            var state = baseState.CreateState(entity?.Status);

            return await state.ConfirmReservation(id);
        }

        public async Task<List<string>> GetAllowedActions(int id)
        {
            var entity = await context.Reservations.FindAsync(id);
            var state = baseState.CreateState(entity?.Status);

            return await state.GetAllowedActions();
        }

        public async Task<Models.Reservation> CancelReservation(int id)
        {
            var entity = await context.Reservations.FindAsync(id);

            var state = baseState.CreateState(entity?.Status);

            return await state.CancelReservation(id);
        }

        public async Task<Models.Reservation> SwitchToPendingConfirmation(int id)
        {
            var entity = await context.Reservations.FindAsync(id);

            var state = baseState.CreateState(entity?.Status);

            return await state.SwitchToPendingConfirmation(id);
        }
    }
}
