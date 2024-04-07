using SPSP.Models.SearchObjects;
using System.Threading.Tasks;
using SPSP.Services.Base;
using SPSP.Models.Request.Order;

namespace SPSP.Services.Order
{
    public interface IOrderService 
        : ICRUDService<Models.Order, OrderSearchObject, OrderCreateRequest, OrderUpdateRequest>
    {
        //Task<Models.Order> Insert();
    }
}
