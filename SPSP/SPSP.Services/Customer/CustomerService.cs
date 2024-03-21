using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Threading.Tasks;
using SPSP.Services.Base;
using SPSP.Models.Request.Customer;
using SPSP.Services.UserAccount;
using SPSP.Models.Request.UserAccount;
using SPSP.Services.MenuItem;

namespace SPSP.Services.Customer
{

    public class CustomerService : BaseCRUDService<Models.Customer, Database.Customer, BaseSearchObject, CustomerCreateRequest, CustomerUpdateRequest>, ICustomerService
    {

        protected readonly IUserAccountService userAccountService;
        protected readonly ICustomerService customerService;

        public CustomerService(DataDbContext context, IMapper mapper, IUserAccountService userAccountService) 
            : base(context, mapper)
        {
            this.userAccountService = userAccountService;
        }
        public override async Task<Models.Customer> Create(CustomerCreateRequest create)
        {
            var customerEntity = mapper.Map<Database.Customer>(create);

            var userAccount = await userAccountService.Create(create);

            customerEntity.UserAccountId = userAccount.Id;

            context.Customers.Add(customerEntity);
            await context.SaveChangesAsync();

            return mapper.Map<Models.Customer>(customerEntity);
        }


    }
}
