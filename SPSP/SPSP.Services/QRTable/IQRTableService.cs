using SPSP.Models.Request.QRTable;
using SPSP.Models.SearchObjects;
using SPSP.Services.Base;

namespace SPSP.Services.QRTable
{
    public interface IQRTableService
        : ICRUDService<Models.QRTable, BaseSearchObject, QRTableCreateRequest, QRTableUpdateRequest>
    {
        public Task<Models.QRTable> SetIsTaken(int qrTableId, bool isTaken);
    }
}
