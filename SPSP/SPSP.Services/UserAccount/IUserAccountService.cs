using SPSP.Models.Request.Customer;
using SPSP.Models.Request.Reservation;
using SPSP.Models.Request.UserAccount;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;
using System.Threading.Tasks;

namespace SPSP.Services.UserAccount
{
    public interface IUserAccountService 
        : ICRUDService<Models.UserAccount, UserAccountSearchObject, UserAccountCreateRequest, UserAccountUpdateRequest>
    {
        public Task<Models.UserAccount> Login(string username, string password);
    }
}
