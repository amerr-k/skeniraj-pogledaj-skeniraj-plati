using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models;
using SPSP.Models.Request.Customer;
using SPSP.Models.SearchObjects;
using SPSP.Services;
using SPSP.Services.Customer;

namespace SPSP.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class CustomerController
        : BaseCRUDController<Models.Customer, BaseSearchObject, CustomerCreateRequest, CustomerUpdateRequest>
    {

        public CustomerController(ILogger<BaseCRUDController<Customer, BaseSearchObject, CustomerCreateRequest, CustomerUpdateRequest>> logger,
            ICustomerService service)
            : base(logger, service)
        {
 
        }
    }
}
