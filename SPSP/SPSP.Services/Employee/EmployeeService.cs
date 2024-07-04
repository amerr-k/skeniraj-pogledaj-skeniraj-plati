using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;
using SPSP.Models.Request.Employee;
using SPSP.Services.UserAccount;
using SPSP.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SPSP.Services.Employee
{

    public class EmployeeService : BaseCRUDService<Models.Employee, Database.Employee, EmployeeSearchObject, EmployeeCreateRequest, EmployeeUpdateRequest>, IEmployeeService
    {

        protected readonly IUserAccountService userAccountService;
        protected readonly IHttpContextAccessor httpContextAccessor;

        public EmployeeService(DataDbContext context, IMapper mapper, IUserAccountService userAccountService, IHttpContextAccessor httpContextAccessor) 
            : base(context, mapper)
        {
            this.userAccountService = userAccountService;
            this.httpContextAccessor = httpContextAccessor;
            //obzirom da smo pozvali base() nepotrebno je ovdje dodavati i deklarisati context i mapper i funkcije
        }

        public override async Task<Models.Employee> Create(EmployeeCreateRequest create)
        {
            var employeeEntity = mapper.Map<Database.Customer>(create);

            var userAccount = await userAccountService.Create(create);

            employeeEntity.UserAccountId = userAccount.Id;

            context.Customers.Add(employeeEntity);
            await context.SaveChangesAsync();

            return mapper.Map<Models.Employee>(employeeEntity);
        }

        public override IQueryable<Database.Employee> AddInclude(IQueryable<Database.Employee> query, EmployeeSearchObject search = null)
        {
            if (search.IsUserAccountIncluded == true)
            {
                query = query.Include(x => x.UserAccount);
            }

            return base.AddInclude(query, search);
        }

        public async Task<Models.Employee> GetAuthenticatedEmployee(string username, string password)
        {
            var employee = await context.Employees
                .Include(x => x.UserAccount)
                .ThenInclude(x => x.UserAccountUserRoles)
                .ThenInclude(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.UserAccount.Username == username);

            if (employee == null)
            {
                return null;
            }

            var hash = userAccountService.GenerateHash(employee.UserAccount.PasswordSalt, password);

            if (hash != employee.UserAccount.PasswordHash)
            {
                return null;
            }

            return mapper.Map<Models.Employee>(employee);
        }

        public Models.Employee GetEmployeeAccountInfo()
        {
            var principal = httpContextAccessor.HttpContext?.User;
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var employee = context.Customers
                .Include(x => x.UserAccount)
                .Where(x => x.UserAccountId == int.Parse(userId!))
                .FirstOrDefault();

            return mapper.Map<Models.Employee>(employee);
        }

    }
}
