using SPSP.Models.Request.QRTable;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.QRTable
{
    public interface IQRTableService 
        : ICRUDService<Models.QRTable, BaseSearchObject, QRTableInsertRequest, QRTableUpdateRequest>
    {

    }
}
