using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPSP.Models;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using System.Threading.Tasks;
using SPSP.Services.Base;
using SPSP.Models.Request.Employee;

namespace SPSP.Services.Employee
{

    public class EmployeeService : BaseCRUDService<Models.Employee, Database.Employee, BaseSearchObject, EmployeeCreateRequest, EmployeeUpdateRequest>, IEmployeeService
    {

        public EmployeeService(DataDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            //obzirom da smo pozvali base() ne potrebno je ovdje dodavati i deklarisati context i mapper i funkcije
        }

        public override async Task PrepareBeforeCreate(Database.Employee entity, EmployeeCreateRequest create)
        {
            //pozovi user account servic
        }

    }
}
