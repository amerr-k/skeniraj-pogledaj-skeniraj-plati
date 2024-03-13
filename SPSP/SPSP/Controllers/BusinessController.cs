using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Controllers
{
    [ApiController] 
    
    public class BusinessController : BaseController<Models.Business, BaseSearchObject>
    {

        public BusinessController(ILogger<BaseController<Models.Business, BaseSearchObject>> logger,
            IService<Models.Business, BaseSearchObject> service)
            : base(logger, service)
        {
 
        }

    }
}
