using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models;
using SPSP.Models.SearchObjects;
using SPSP.Services.Order;

namespace SPSP.Controllers
{
    [ApiController]
    public class OrderController : BaseController<Models.Order, OrderSearchObject>
    {
        public OrderController(ILogger<BaseController<Order, OrderSearchObject>> logger,
            IOrderService service)
            : base(logger, service)
        {
 
        }
    }
}


//[HttpPost]
//public async Task<Models.Order> Insert()
//{
//    return await (service as IOrderService).Insert(); 
//bolje je da ako ce se ovaj servis koristit umjesto casta kreirati properti service na nivou ovog kontrolera
//}

//[HttpPost]
//public async Task<Models.Order> Insert()
//{
//    return await service.Insert();
//    
//}