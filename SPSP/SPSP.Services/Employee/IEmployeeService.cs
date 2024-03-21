using SPSP.Models.Request.Employee;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.Employee
{
    public interface IEmployeeService 
        : ICRUDService<Models.Employee, BaseSearchObject, EmployeeCreateRequest, EmployeeUpdateRequest>
    {
        
    }
}
