using AutoMapper;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Linq;
using System.Threading.Tasks;
using SPSP.Services.Base;
using SPSP.Models.Request.Reservation;
using SPSP.Models.Request.UserAccount;
using System.Security.Cryptography;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SPSP.Models.Request.Customer;

namespace SPSP.Services.UserAccount
{

    public class UserAccountService : BaseCRUDService<Models.UserAccount, Database.UserAccount, UserAccountSearchObject, UserAccountCreateRequest, UserAccountUpdateRequest>, IUserAccountService
    {

        public UserAccountService(DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            
        }

        //public override async Task<Models.UserAccount> Create(UserAccountCreateRequest create)
        //{
        //    var userAccountEntity = mapper.Map<Database.UserAccount>(create);
        //    context.UserAccounts.Add(userAccountEntity);

        //    await PrepareBeforeCreate(userAccountEntity, create);

        //    await context.SaveChangesAsync();
        //    return mapper.Map<Models.UserAccount>(userAccountEntity);
        //}

        public override async Task PrepareBeforeCreate(Database.UserAccount entity, UserAccountCreateRequest create)
        {
            entity.PasswordSalt = GenerateSalt();
            entity.PasswordHash = GenerateHash(entity.PasswordSalt, create.Password);
        }

        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            provider.GetBytes(byteArray);


            return Convert.ToBase64String(byteArray);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }


        public override IQueryable<Database.UserAccount> AddInclude(IQueryable<Database.UserAccount> query, UserAccountSearchObject? search = null)
        {
            if (search?.IsUserRolesIncluded == true)
            {
                query = query.Include("UserAccountUserRoles.UserRole");
            }
            return base.AddInclude(query, search);
        }
        public async Task<Models.UserAccount> Login(string username, string password)
        {
            var entity = await context.UserAccounts.Include("UserAccountUserRoles.UserRole").FirstOrDefaultAsync(x => x.Username == username);

            if (entity == null)
            {
                return null;
            }

            var hash = GenerateHash(entity.PasswordSalt, password);

            if (hash != entity.PasswordHash)
            {
                return null;
            }

            return mapper.Map<Models.UserAccount>(entity);
        }

    }
}
