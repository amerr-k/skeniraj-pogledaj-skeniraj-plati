using SPSP.Models.SearchObjects;
using System.Threading.Tasks;
using SPSP.Services.Base;

namespace SPSP.Services.Order
{
    public interface IOrderService 
        : IService<Models.Order, OrderSearchObject>
    {
        Task<Models.Order> Insert();
    }
}
