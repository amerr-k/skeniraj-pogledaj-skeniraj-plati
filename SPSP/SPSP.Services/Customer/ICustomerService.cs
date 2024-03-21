using SPSP.Models.Request.Customer;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.Customer
{
    public interface ICustomerService 
        : ICRUDService<Models.Customer, BaseSearchObject, CustomerCreateRequest, CustomerUpdateRequest>
    {
        
    }
}
