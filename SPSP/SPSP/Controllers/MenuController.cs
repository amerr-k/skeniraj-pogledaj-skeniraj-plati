using Microsoft.AspNetCore.Mvc;
using SPSP.Controllers.Base;
using SPSP.Models.Request.MenuItem;
using SPSP.Models.SearchObjects;
using SPSP.Services.Menu;

namespace SPSP.Controllers
{
    [ApiController]
    public class MenuController : BaseCRUDController<Models.Menu, MenuSearchObject, MenuCreateRequest, MenuUpdateRequest>
    {

        public MenuController(ILogger<BaseCRUDController<Models.Menu, MenuSearchObject, MenuCreateRequest, MenuUpdateRequest>> logger,
            IMenuService service)
            : base(logger, service)
        {
 
        }

    }
}
