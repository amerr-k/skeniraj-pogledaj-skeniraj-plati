using SPSP.Models.SearchObjects;
using System.Threading.Tasks;
using SPSP.Services.Base;
using SPSP.Models.Request.Order;
using SPSP.Models.Enums;

namespace SPSP.Services.Order
{
    public interface IOrderService 
        : ICRUDService<Models.Order, OrderSearchObject, OrderCreateRequest, OrderUpdateRequest>
    {
        Task<Models.Order> UpdateStatus(int orderId, OrderStatusEnum orderStatus, int customerId);
    }
}
