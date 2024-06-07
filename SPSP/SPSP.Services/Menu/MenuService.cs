using AutoMapper;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.Request.MenuItem;
using SPSP.Models.Enums;
using SPSP.Models.Request.Order;
using SPSP.Services.QRTable;

namespace SPSP.Services.Menu
{
    public class MenuService : BaseCRUDService<Models.Menu, Database.Menu, MenuSearchObject, MenuCreateRequest, MenuUpdateRequest>, IMenuService
    {

        public MenuService(DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
           
        }

        public override async Task<Models.Menu> Create(MenuCreateRequest create)
        {

            var menuEntity = mapper.Map<Database.Menu>(create);

            if(create.IsActive == true)
            {
                await DeactivateLastActiveMenu();
            }

            context.Menus.Add(menuEntity);
            await context.SaveChangesAsync();

            var menu = mapper.Map<Models.Menu>(menuEntity);

            return menu;
        }

        public override async Task<Models.Menu> Update(int id, MenuUpdateRequest update)
        {
            if (update.IsActive == true)
            {
                await DeactivateLastActiveMenu();
            }

            var menuEntity = await context.Menus.FindAsync(id);
            if (menuEntity != null)
            {
                menuEntity.Name = update.Name;
                menuEntity.IsActive = update.IsActive;
            }
            await context.SaveChangesAsync();

            return mapper.Map<Models.Menu>(menuEntity);
        }

        private async Task DeactivateLastActiveMenu()
        {
            var lastActive = await context.Menus.Where(x => x.IsActive == true).FirstOrDefaultAsync();
            if(lastActive != null)
            {
                lastActive.IsActive = false;
            }
            await context.SaveChangesAsync();
        }

        public override IQueryable<Database.Menu> AddInclude(IQueryable<Database.Menu> query, MenuSearchObject search = null)
        {
            if (search.IsMenuItemsIncluded == true)
            {
                query = query.Include(x => x.MenuItems);
            }

            return base.AddInclude(query, search);
        }

        public override IQueryable<Database.Menu> AddFilter(IQueryable<Database.Menu> query, MenuSearchObject search)
        {
            if (!string.IsNullOrWhiteSpace(search?.Name))
            {
                query = query.Where(x => x.Name.StartsWith(search.Name));
            }

            return base.AddFilter(query, search);
        }

    }
}
