using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;
using SPSP.Models.Request.MenuItem;
using SPSP.Services.RecommenderService;

namespace SPSP.Services.MenuItem
{

    public class MenuItemService : BaseCRUDService<Models.MenuItem, Database.MenuItem, MenuItemSearchObject, MenuItemCreateRequest, MenuItemUpdateRequest>, IMenuItemService
    {
        
        public MenuItemService(DataDbContext context, IMapper mapper, IRecommenderService recommenderService)
            : base(context, mapper)
        {
        }

        public override async Task PrepareBeforeCreate(Database.MenuItem entity, MenuItemCreateRequest insert)
        {
            
        }

        public override async Task<Models.MenuItem> GetById(int id)
        {
            var entity = await context.Set<Database.MenuItem>()
                .Include(x => x.Category)
                .Include(x => x.Menu)
                .FirstOrDefaultAsync(x => x.Id == id);

            var model = mapper.Map<Models.MenuItem>(entity);

            return model;
        }

        public override IQueryable<Database.MenuItem> AddFilter(IQueryable<Database.MenuItem> query, MenuItemSearchObject search)
        {
            if (!string.IsNullOrWhiteSpace(search?.Name))
            {
                query = query.Where(x => x.Name.StartsWith(search.Name));
            }

            if (!string.IsNullOrWhiteSpace(search?.FTS))
            {
                query = query.Where(x => x.Name.Contains(search.FTS) || x.Code.Contains(search.FTS) || x.Description.Contains(search.FTS));
            }

            if(search?.MenuId != null)
            {
                query = query.Where(x => x.MenuId == search.MenuId);
            } 

            if(search?.IsMenuActive != null)
            {
                query = query.Include(x => x.Menu).Where(y => y.Menu.IsActive == search.IsMenuActive);

            }

            return base.AddFilter(query, search);
        }

        public override IQueryable<Database.MenuItem> AddInclude(IQueryable<Database.MenuItem> query, MenuItemSearchObject search = null)
        {
            if (search.IsCategoryIncluded == true)
            {
                query = query.Include(x => x.Category);
            }
            if (search.IsMenuIncluded == true)
            {
                query = query.Include(x => x.Menu);
            }

            return base.AddInclude(query, search);
        }
    }
}

