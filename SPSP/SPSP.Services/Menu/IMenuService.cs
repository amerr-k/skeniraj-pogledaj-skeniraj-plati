using SPSP.Models.Request.MenuItem;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.Menu
{
    public interface IMenuService : ICRUDService<Models.Menu, MenuSearchObject, MenuCreateRequest, MenuUpdateRequest>
    {

    }
}
