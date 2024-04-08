using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSP.Services.Database.SeedData
{
    public static class OrderData
    {
        public static void SeedData(this EntityTypeBuilder<Order> entity)
        {
            entity.HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)8.3,
                    TotalAmountWithVAT = 10,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)1.7,
                    Status = "COMPLETED",
                    QRTableId = 2,
                    Valid = true
                }
                ,
                new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)12.45,
                    TotalAmountWithVAT = 15,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)2.55,
                    Status = "COMPLETED",
                    QRTableId = 3,
                    Valid = true
                }
                ,
                new Order
                {
                    Id = 3,
                    CustomerId = 3,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)15.6,
                    TotalAmountWithVAT = 20,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)3.4,
                    Status = "COMPLETED",
                    QRTableId = 1,
                    Valid = true
                }
                ,
                new Order
                {
                    Id = 4,
                    CustomerId = 1,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)8.3,
                    TotalAmountWithVAT = 10,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)1.7,
                    Status = "COMPLETED",
                    QRTableId = 5,
                    Valid = true
                },
                new Order
                {
                    Id = 5,
                    CustomerId = 2,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)12.45,
                    TotalAmountWithVAT = 15,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)2.55,
                    Status = "COMPLETED",
                    QRTableId = 1,
                    Valid = true
                },
                new Order
                {
                    Id = 6,
                    CustomerId = 3,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)15.6,
                    TotalAmountWithVAT = 20,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)3.4,
                    Status = "COMPLETED",
                    QRTableId = 4,
                    Valid = true
                },
                new Order
                {
                    Id = 7,
                    CustomerId = 1,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)8.3,
                    TotalAmountWithVAT = 10,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)1.7,
                    Status = "COMPLETED",
                    QRTableId = 1,
                    Valid = true
                },
                new Order
                {
                    Id = 8,
                    CustomerId = 2,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)12.45,
                    TotalAmountWithVAT = 15,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)2.55,
                    Status = "COMPLETED",
                    QRTableId = 4,
                    Valid = true
                },
                new Order
                {
                    Id = 9,
                    CustomerId = 3,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)15.6,
                    TotalAmountWithVAT = 20,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)3.4,
                    Status = "COMPLETED",
                    QRTableId = 2,
                    Valid = true
                },
                new Order
                {
                    Id = 10,
                    CustomerId = 1,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)8.3,
                    TotalAmountWithVAT = 10,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)1.7,
                    Status = "ACTIVE",
                    QRTableId = 3,
                    Valid = true
                },
                new Order
                {
                    Id = 11,
                    CustomerId = 2,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)12.45,
                    TotalAmountWithVAT = 15,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)2.55,
                    Status = "ACTIVE",
                    QRTableId = 5,
                    Valid = true
                },
                new Order
                {
                    Id = 12,
                    CustomerId = 3,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)15.6,
                    TotalAmountWithVAT = 20,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)3.4,
                    Status = "ACTIVE",
                    QRTableId = 5,
                    Valid = true
                },
                new Order
                {
                    Id = 13,
                    CustomerId = 1,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)8.3,
                    TotalAmountWithVAT = 10,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)1.7,
                    Status = "CANCELED",
                    QRTableId = 5,
                    Valid = true
                },
                new Order
                {
                    Id = 14,
                    CustomerId = 2,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)12.45,
                    TotalAmountWithVAT = 15,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)2.55,
                    Status = "CANCELED",
                    QRTableId = 5,
                    Valid = true
                },
                new Order
                {
                    Id = 15,
                    CustomerId = 3,
                    OrderDateTime = DateTime.Now,
                    TotalAmount = (decimal)15.6,
                    TotalAmountWithVAT = 20,
                    VAT = (decimal)0.17,
                    VATAmount = (decimal)3.4,
                    Status = "CANCELED",
                    QRTableId = 5,
                    Valid = true
                }
            );
        }
    }
}




