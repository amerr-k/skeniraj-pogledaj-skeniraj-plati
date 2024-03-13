using AutoMapper;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;

namespace SPSP.Services.Reservation
{
    public class ReservationService : BaseService<Models.Reservation, Database.Reservation, BaseSearchObject>, IReservationService
    {

        public ReservationService(DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
           
        }
    }
}
