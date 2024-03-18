using SPSP.Models.Request.Reservation;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPSP.Services.Reservation
{
    public interface IReservationService
        : ICRUDService<Models.Reservation, BaseSearchObject, ReservationCreateRequest, ReservationUpdateRequest>
    {
        public Task<Models.Reservation> ConfirmReservation(int id);
        public Task<Models.Reservation> CancelReservation(int id);
        public Task<Models.Reservation> PutReservationOnHold(ReservationCreateRequest create);
        public Task<List<string>> GetAllowedActions(int id);
    }
}
