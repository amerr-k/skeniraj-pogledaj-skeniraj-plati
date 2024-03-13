using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models;
using SPSP.Models.SearchObjects;
using SPSP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPSP.Services.Menu;

namespace SPSP.Controllers
{
    [ApiController]
    public class MenuController : BaseController<Models.Menu, MenuSearchObject>
    {

        public MenuController(ILogger<BaseController<Menu, MenuSearchObject>> logger,
            IMenuService service)
            : base(logger, service)
        {
 
        }

    }
}
