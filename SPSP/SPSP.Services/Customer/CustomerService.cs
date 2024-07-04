using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;
using SPSP.Models.Request.Customer;
using SPSP.Services.UserAccount;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SPSP.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SPSP.Services.Customer
{

    public class CustomerService : BaseCRUDService<Models.Customer, Database.Customer, CustomerSearchObject, CustomerCreateRequest, CustomerUpdateRequest>, ICustomerService
    {

        protected readonly IUserAccountService userAccountService;
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public CustomerService(DataDbContext context, IMapper mapper, IUserAccountService userAccountService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(context, mapper)
        {
            this.userAccountService = userAccountService;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
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

        public override async Task<Models.Customer> Update(int id, CustomerUpdateRequest update)
        {
            var customerEntity = mapper.Map<Database.Customer>(update);

            await userAccountService.Update(update.UserAccountId, update);

            context.Customers.Add(customerEntity);
            await context.SaveChangesAsync();

            return mapper.Map<Models.Customer>(customerEntity);
        }

        public override IQueryable<Database.Customer> AddInclude(IQueryable<Database.Customer> query, CustomerSearchObject search = null)
        {
            if (search.IsUserAccountIncluded == true)
            {
                query = query.Include(x => x.UserAccount);
            }

            return base.AddInclude(query, search);
        }

        public Models.Customer GetCustomerAccountInfo()
        {
            var principal = httpContextAccessor.HttpContext?.User;
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var customer = context.Customers
                .Include(x => x.UserAccount)
                .Where(x => x.UserAccountId == int.Parse(userId!))
                .FirstOrDefault();

            return mapper.Map<Models.Customer>(customer);
        }

        public async Task<UserAuthInfo> Register(CustomerCreateRequest customerCreateRequest)
        {
            var userAccount = await userAccountService.Create(customerCreateRequest);

            var customer = new Database.Customer { UserAccountId = userAccount.Id };

            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            return new UserAuthInfo(userAccount, "");
        }

        public async Task<Models.Customer> GetAuthenticatedCustomer(string username, string password)
        {
            var customer = await context.Customers
                .Include(x => x.UserAccount)
                .ThenInclude(x => x.UserAccountUserRoles)
                .ThenInclude(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.UserAccount.Username == username);

            if (customer == null)
            {
                return null;
            }

            var hash = userAccountService.GenerateHash(customer.UserAccount.PasswordSalt, password);

            if (hash != customer.UserAccount.PasswordHash)
            {
                return null;
            }

            return mapper.Map<Models.Customer>(customer);
        }

        public async Task<Models.UserAuthInfo> MobileLogin(string username, string password)
        {

            var customer = await GetAuthenticatedCustomer(username, password);

            if (customer == null || customer.UserAccount == null)
            {
                throw new AppException("Unijeli ste pogrešne kredencijale.");
            }

            var jwtToken = GenerateJwtToken(customer.UserAccount);

            return new UserAuthInfo(customer.UserAccount, jwtToken);
        }

        public string GenerateJwtToken(Models.UserAccount userAccount)
        {
            var issuer = configuration.GetValue<string>("TokenConfig:Issuer");
            var audience = configuration.GetValue<string>("TokenConfig:Audience");
            var signingKey = configuration.GetValue<string>("TokenConfig:SigningKey");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userAccount.Username),
                new Claim(ClaimTypes.Email, userAccount.Email),
                new Claim(ClaimTypes.NameIdentifier, userAccount.Id.ToString()),
             };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
