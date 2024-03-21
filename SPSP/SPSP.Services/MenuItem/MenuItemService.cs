using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Linq;
using System.Threading.Tasks;
using SPSP.Services.Base;

namespace SPSP.Services.MenuItem
{

    // dali menuitem service uopste treba naslijediti IMenuItemService, on vec nasljeduje IMenuItemService
    //prvo cu probat MenuItemService ovako pa cu onda zakomentarisats
    public class MenuItemService : BaseCRUDService<Models.MenuItem, Database.MenuItem, MenuItemSearchObject, MenuItemCreateRequest, MenuItemUpdateRequest>, IMenuItemService
    {

        public MenuItemService(DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            //obzirom da smo pozvali base() ne potrebno je ovdje dodavati i deklarisati context i mapper i funkcije
        }

        public override async Task PrepareBeforeCreate(Database.MenuItem entity, MenuItemCreateRequest insert)
        {
            //URADI NESTO SPECIFICNO ZA MENU ITEM SERVICE NPR DODAJ U ATRIBUT
        }



        public override IQueryable<Database.MenuItem> AddFilter(IQueryable<Database.MenuItem> query, MenuItemSearchObject search)
        {
            if (!string.IsNullOrWhiteSpace(search?.Name))
            {
                query = query.Where(x => x.Name.StartsWith(search.Name));
            }

            if (!string.IsNullOrWhiteSpace(search?.FTS))
            {
                query = query.Where(x => x.Name.Contains(search.FTS));
            }

            return base.AddFilter(query, search);
        }

    }
}
