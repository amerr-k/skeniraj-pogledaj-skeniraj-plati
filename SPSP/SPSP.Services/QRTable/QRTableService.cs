using AutoMapper;
using SPSP.Models.Request.QRTable;
using SPSP.Models.SearchObjects;
using SPSP.Services.Database;
using SPSP.Services.Base;

namespace SPSP.Services.QRTable
{
    public class QRTableService : BaseCRUDService<Models.QRTable, Database.QRTable, BaseSearchObject, QRTableCreateRequest, QRTableUpdateRequest>, IQRTableService
    {
        public QRTableService(DataDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }

        public async Task<Models.QRTable> SetIsTaken(int qrTableId, bool isTaken)
        {
            var qrTableEntity = await context.QRTables.FindAsync(qrTableId);
            if (qrTableEntity != null)
            {
                qrTableEntity.IsTaken = isTaken;
            }

            await context.SaveChangesAsync();

            return mapper.Map<Models.QRTable>(qrTableEntity);   
        }
    }
}
