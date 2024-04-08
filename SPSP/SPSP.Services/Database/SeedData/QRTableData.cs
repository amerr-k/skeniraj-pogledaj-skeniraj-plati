using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSP.Services.Database.SeedData
{
    public static class QRTableData
    {
        public static void SeedData(this EntityTypeBuilder<QRTable> entity)
        {
            entity.HasData(
                new QRTable
                {
                    Id = 1,
                    QRCode = "QRCODEEXAMPLETEST1",
                    TableNumber = 1,
                    Capacity = 4,
                    LocationDescription = "Uz prozore",
                    IsTaken = false,
                    Valid = true
                },
                new QRTable
                {
                    Id = 2,
                    QRCode = "QRCODEEXAMPLETEST2",
                    TableNumber = 2,
                    Capacity = 4,
                    LocationDescription = "Uz šank",
                    IsTaken = false,
                    Valid = true
                },
                new QRTable
                {
                    Id = 3,
                    QRCode = "QRCODEEXAMPLETEST3",
                    TableNumber = 3,
                    Capacity = 4,
                    LocationDescription = "Centar",
                    IsTaken = true,
                    Valid = true
                },
                new QRTable
                {
                    Id = 4,
                    QRCode = "QRCODEEXAMPLETEST4",
                    TableNumber = 4,
                    Capacity = 4,
                    LocationDescription = "Centar",
                    IsTaken = true,
                    Valid = true
                },
                new QRTable
                {
                    Id = 5,
                    QRCode = "QRCODEEXAMPLETEST5",
                    TableNumber = 5,
                    Capacity = 4,
                    LocationDescription = "Uz prozore",
                    IsTaken = true,
                    Valid = true
                }
            );
        }
    }
}




