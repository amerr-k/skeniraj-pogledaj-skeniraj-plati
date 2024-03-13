using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Services.Base;
using System.Threading.Tasks;

namespace SPSP.Controllers.Base
{
    [Route("[controller]")]
    public class BaseCRUDController<T, TSearch, TInsert, TUpdate> 
        : BaseController<T, TSearch> 
        where T : class where TSearch : class where TInsert : class where TUpdate : class
    {
        protected readonly ILogger<BaseCRUDController<T, TSearch, TInsert, TUpdate>> logger; //logger nam ovdje sad za sad ne treba, jer ga već ima u nasljedjenom BaseControlleru
        protected new readonly ICRUDService<T, TSearch, TInsert, TUpdate> service;

        public BaseCRUDController(ILogger<BaseCRUDController<T, TSearch, TInsert, TUpdate>> logger, ICRUDService<T, TSearch, TInsert, TUpdate> service)
            : base(logger, service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<T> Insert([FromBody]TInsert insert)
        {
            return await service.Insert(insert);
        }

        [HttpPut("{id}")]
        public async Task<T> Update(int id, [FromBody] TUpdate update)
        {
            return await service.Update(id, update);
        }
    }
}
