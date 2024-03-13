using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Linq;
using System.Threading.Tasks;
using SPSP.Services.Base;

namespace SPSP.Services.Order
{
    public class OrderService : BaseService<Models.Order, Database.Order, OrderSearchObject>, IOrderService
    {

        public OrderService(DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
           
        }

        public override IQueryable<Database.Order> AddInclude(IQueryable<Database.Order> query, OrderSearchObject search = null)
        {
            if (search.IsOrderItemsIncluded == true)
            {
                query = query.Include(x => x.OrderItems) 
                            .ThenInclude(o => o.MenuItem)
                            .ThenInclude(c => c.Category);
            }

            return base.AddInclude(query, search);
        }

        public async Task<Models.Order> Insert()
        {
            return null;
        }

        //public override IQueryable<Database.OrderItem> AddFilter(IQueryable<Database.OrderItem> query, OrderItemSearchObject search)
        //{
        //    if (!string.IsNullOrWhiteSpace(search?.Name))
        //    {
        //        query = query.Where(x => x.Name.StartsWith(search.Name));
        //    }

        //    if (!string.IsNullOrWhiteSpace(search?.FTS))
        //    {
        //        query = query.Where(x => x.Name.Contains(search.FTS));
        //    }

        //    return base.AddFilter(query, search);
        //}

    }
}
