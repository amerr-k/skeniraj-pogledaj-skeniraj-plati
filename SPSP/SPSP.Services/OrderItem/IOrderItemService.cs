using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.OrderItem
{
    public interface IOrderItemService 
        : IService<Models.OrderItem, BaseSearchObject>
    {
    }
}
