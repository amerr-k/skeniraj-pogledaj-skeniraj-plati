using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Linq;
using SPSP.Services.Base;

namespace SPSP.Services.OrderItem
{
    public class OrderItemService : BaseService<Models.OrderItem, Database.OrderItem, BaseSearchObject>, IOrderItemService
    {

        public OrderItemService(DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
           
        }
    }
}
