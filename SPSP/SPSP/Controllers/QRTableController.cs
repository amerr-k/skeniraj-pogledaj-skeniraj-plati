using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models;
using SPSP.Models.Request.QRTable;
using SPSP.Models.SearchObjects;
using SPSP.Services.QRTable;
namespace SPSP.Controllers
{
    [ApiController]
    public class QRTableController : BaseCRUDController<QRTable, BaseSearchObject, QRTableCreateRequest, QRTableUpdateRequest>
    {
        public QRTableController(ILogger<BaseCRUDController<QRTable, BaseSearchObject, QRTableCreateRequest, QRTableUpdateRequest>> logger, IQRTableService service) 
            : base(logger, service)
        {

        }
    }
}
