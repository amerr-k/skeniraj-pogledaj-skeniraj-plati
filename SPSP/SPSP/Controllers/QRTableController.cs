using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SPSP.Controllers.Base;
using SPSP.Models.Request.QRTable;
using SPSP.Models.SearchObjects;
using SPSP.Services.QRTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SPSP.Controllers
{
    [ApiController]
    public class QRTableController : BaseCRUDController<Models.QRTable, BaseSearchObject, QRTableInsertRequest, QRTableUpdateRequest>
    {
        public QRTableController(ILogger<BaseCRUDController<Models.QRTable, BaseSearchObject, QRTableInsertRequest, QRTableUpdateRequest>> logger, IQRTableService service) 
            : base(logger, service)
        {

        }
    }
}
