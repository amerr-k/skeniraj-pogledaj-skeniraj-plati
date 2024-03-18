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
        Task<Models.Reservation> ConfirmReservation(int id);
        Task<Models.Reservation> CancelReservation(int id);
        Task<Models.Reservation> PutReservationOnHold(ReservationCreateRequest create);
        Task<List<string>> GetAllowedActions(int id);
        Task<Models.Reservation> SwitchToPendingConfirmation(int id);
    }
}
