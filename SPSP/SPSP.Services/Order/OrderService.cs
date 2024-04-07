using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Linq;
using System.Threading.Tasks;
using SPSP.Services.Base;
using SPSP.Models.Request.Order;
using SPSP.Models.Request.Employee;
using SPSP.Services.UserAccount;
using SPSP.Services.OrderItem;
using SPSP.Services.QRTable;

namespace SPSP.Services.Order
{
    public class OrderService 
        : BaseCRUDService<Models.Order, Database.Order, OrderSearchObject, OrderCreateRequest, OrderUpdateRequest>, IOrderService
    {
        protected readonly IOrderItemService orderItemService;
        protected readonly IQRTableService qrTableService;

        public OrderService(DataDbContext context, IMapper mapper, IOrderItemService orderItemService, IQRTableService qrTableService) 
            : base(context, mapper)
        {
            this.orderItemService = orderItemService;
            this.qrTableService = qrTableService;
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

        public override async Task<Models.Order> Create(OrderCreateRequest create)
        {
            var orderEntity = mapper.Map<Database.Order>(create);
            orderEntity.Status = "CREATED";

            context.Orders.Add(orderEntity);
            await context.SaveChangesAsync();

            List<Models.OrderItem> orderItems;
            var order = mapper.Map<Models.Order>(orderEntity);
            
            if (create.OrderItems  != null)
            {
                orderItems = await orderItemService.CreateMultiple(create.OrderItems, orderEntity.Id);
                order.OrderItems = orderItems;
            }

            await qrTableService.SetIsTaken(create.QRTableId, true);

            return order;
        }

        //public Task<Models.Order> Create(OrderUpdateRequest create)
        //{
        //    throw new NotImplementedException();
        //}

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
