using AutoMapper;
using SPSP.Models.Request.QRTable;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;

namespace SPSP.Services.QRTable
{
    public class QRTableService : BaseCRUDService<Models.QRTable, Database.QRTable, BaseSearchObject, QRTableInsertRequest, QRTableUpdateRequest>, IQRTableService
    {
        public QRTableService(DataDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }
    }
}
