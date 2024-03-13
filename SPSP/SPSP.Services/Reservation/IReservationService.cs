using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.Reservation
{
    public interface IReservationService 
        : IService<Models.Reservation, BaseSearchObject>
    {
    }
}
