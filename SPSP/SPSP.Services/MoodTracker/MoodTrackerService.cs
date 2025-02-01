using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;
using SPSP.Services.RecommenderService;
using SPSP.Models.Request.MoodTracker;
using SPSP.Models.Enums;

namespace SPSP.Services.MoodTracker
{


    public class MoodTrackerService : BaseCRUDService<Models.MoodTracker, Database.MoodTracker, MoodTrackerSearchObject, MoodTrackerCreateRequest, MoodTrackerUpdateRequest>, IMoodTrackerService
    {
        public int GLOBAL_COUNTER = 0;
        public DateTime GLOBAL_DATE;

        public MoodTrackerService(DataDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public override Task PrepareBeforeCreate(Database.MoodTracker db, MoodTrackerCreateRequest create)
        {

            if (GLOBAL_DATE == DateTime.Today.Date)
            {
                GLOBAL_COUNTER++;
            }
            else
            {
                GLOBAL_COUNTER = 1;
            }

            GLOBAL_DATE = DateTime.Today;

            if (GLOBAL_COUNTER >= 2)
            {
                throw new ApplicationException("Ne mozete napraviti novi mood tracker");
            }

            db.MoodTrackerStatus = MoodTrackerStatusEnumExtension.GetValue(create.MoodTrackerStatusEnum);
            return base.PrepareBeforeCreate(db, create);
        }

        public override Task PrepareBeforeUpdate(Database.MoodTracker db, MoodTrackerUpdateRequest update)
        {
            db.MoodTrackerStatus = MoodTrackerStatusEnumExtension.GetValue(update.MoodTrackerStatusEnum);
            return base.PrepareBeforeUpdate(db, update);
        }


        public override IQueryable<Database.MoodTracker> AddFilter(IQueryable<Database.MoodTracker> query, MoodTrackerSearchObject search)
        {
            if (search?.DatumEvidencijeSearch != null)
            {
                query = query.Where(x => x.DatumEvidencije.Date == search.DatumEvidencijeSearch.Value.Date);
            }

            if (search?.MoodTrackerStatusEnum != null)
            {
                query = query.Where(
                    x => x.MoodTrackerStatus.Equals(MoodTrackerStatusEnumExtension.GetValue(search.MoodTrackerStatusEnum.Value)));
            }

            if (search?.CustomerId != null)
            {
                query = query.Where(x => x.CustomerId == search.CustomerId);
            }

            return base.AddFilter(query, search);
        }

        public override IQueryable<Database.MoodTracker> AddInclude(IQueryable<Database.MoodTracker> query, MoodTrackerSearchObject search = null)
        {

            query = query.Include(x => x.Customer).ThenInclude(x => x.UserAccount);

            return base.AddInclude(query, search);
        }
    }


}

