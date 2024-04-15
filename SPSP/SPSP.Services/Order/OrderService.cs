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
            orderEntity.Status = "ACTIVE";

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

        public override IQueryable<Database.Order> AddFilter(IQueryable<Database.Order> query, OrderSearchObject search)
        {
            if (search?.orderStatus != null)
            {
                query = query.Where(x => x.Status.Equals(search.orderStatus.ToString()));
            }

            if (search?.orderDateTimeFrom != null || search?.orderDateTimeTo != null)
            {
                if (search.orderDateTimeFrom != null && search.orderDateTimeTo != null)
                {
                    query = query.Where(x => x.OrderDateTime >= search.orderDateTimeFrom && x.OrderDateTime <= search.orderDateTimeTo);
                }
                else if (search.orderDateTimeFrom != null)
                {
                    query = query.Where(x => x.OrderDateTime >= search.orderDateTimeFrom);
                }
                else if (search.orderDateTimeTo != null)
                {
                    query = query.Where(x => x.OrderDateTime <= search.orderDateTimeTo);
                }
            }

            if (search?.QRTableId != null)
            {
                query = query.Where(x => x.QRTableId == search.QRTableId);
            }


            return base.AddFilter(query, search);
        }

    }
}
