using AutoMapper;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Threading.Tasks;

namespace SPSP.Services.Base
{
    public class BaseCRUDService<T, TDb, TSearch, TInsert, TUpdate> 
        : BaseService<T, TDb, TSearch> 
        where TDb : class where T : class 
        where TSearch : BaseSearchObject
    {

        public BaseCRUDService(DataDbContext context, IMapper mapper)
           : base(context, mapper)
        {

        }

        public virtual async Task PrepareBeforeInsert(TDb db, TInsert insert)
        {
            
        }

        public virtual async Task<T> Insert(TInsert insert)
        {
            var set = context.Set<TDb>();

            var entity = mapper.Map<TDb>(insert);

            set.Add(entity);

            await PrepareBeforeInsert(entity, insert);

            await context.SaveChangesAsync();

            return mapper.Map<T>(entity);
        }

        public virtual async Task<T> Update(int id, TUpdate update)
        {

            var set = context.Set<TDb>();

            var entity = await set.FindAsync(id);

            mapper.Map(update, entity);

            await context.SaveChangesAsync();

            return mapper.Map<T>(entity);
        }

    }
}


//public Models.MenuItem Insert(MenuItemInsertRequest request)
//{
//    var entity = new Database.MenuItem();
//    mapper.Map(request, entity);

//    context.MenuItems.Add(entity);
//    context.SaveChanges();

//    return mapper.Map<Models.MenuItem>(entity);
//}