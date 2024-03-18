using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models;
using SPSP.Models.SearchObjects;
using SPSP.Services;
using SPSP.Services.MenuItem;

namespace SPSP.Controllers
{
    [ApiController] // mora ostati u konkretnom kontroleru zbog swaggera
    public class MenuItemController
        : BaseCRUDController<Models.MenuItem, MenuItemSearchObject, MenuItemCreateRequest, MenuItemUpdateRequest>
    {

        public MenuItemController(ILogger<BaseCRUDController<MenuItem, MenuItemSearchObject, MenuItemCreateRequest, MenuItemUpdateRequest>> logger,
            IMenuItemService service)
            : base(logger, service)
        {
 
        }
    }
}
