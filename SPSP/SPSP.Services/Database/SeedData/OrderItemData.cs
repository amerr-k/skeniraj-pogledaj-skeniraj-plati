using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSP.Services.Database.SeedData
{
    public static class OrderItemData
    {
        public static void SeedData(this EntityTypeBuilder<OrderItem> entity)
        {
            entity.HasData(
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    MenuItemId = 16,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    MenuItemId = 1,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 3,
                    OrderId = 2,
                    MenuItemId = 9,
                    Quantity = 2,
                    Subtotal = 10,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 4,
                    OrderId = 2,
                    MenuItemId = 15,
                    Quantity = 1,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 5,
                    OrderId = 3,
                    MenuItemId = 8,
                    Quantity = 2,
                    Subtotal = 6,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 6,
                    OrderId = 3,
                    MenuItemId = 7,
                    Quantity = 7,
                    Subtotal = 14,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 7,
                    OrderId = 4,
                    MenuItemId = 16,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 8,
                    OrderId = 4,
                    MenuItemId = 1,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 9,
                    OrderId = 5,
                    MenuItemId = 9,
                    Quantity = 2,
                    Subtotal = 10,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 10,
                    OrderId = 5,
                    MenuItemId = 15,
                    Quantity = 1,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 11,
                    OrderId = 6,
                    MenuItemId = 8,
                    Quantity = 2,
                    Subtotal = 6,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 12,
                    OrderId = 6,
                    MenuItemId = 7,
                    Quantity = 7,
                    Subtotal = 14,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 13,
                    OrderId = 7,
                    MenuItemId = 16,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 14,
                    OrderId = 7,
                    MenuItemId = 1,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 15,
                    OrderId = 8,
                    MenuItemId = 9,
                    Quantity = 2,
                    Subtotal = 10,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 16,
                    OrderId = 8,
                    MenuItemId = 15,
                    Quantity = 1,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 17,
                    OrderId = 9,
                    MenuItemId = 8,
                    Quantity = 2,
                    Subtotal = 6,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 18,
                    OrderId = 9,
                    MenuItemId = 7,
                    Quantity = 7,
                    Subtotal = 14,
                    Valid = true
                },

                new OrderItem
                {
                    Id = 19,
                    OrderId = 10,
                    MenuItemId = 16,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 20,
                    OrderId = 10,
                    MenuItemId = 1,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 21,
                    OrderId = 11,
                    MenuItemId = 9,
                    Quantity = 2,
                    Subtotal = 10,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 22,
                    OrderId = 11,
                    MenuItemId = 15,
                    Quantity = 1,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 23,
                    OrderId = 12,
                    MenuItemId = 8,
                    Quantity = 2,
                    Subtotal = 6,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 24,
                    OrderId = 12,
                    MenuItemId = 7,
                    Quantity = 7,
                    Subtotal = 14,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 25,
                    OrderId = 13,
                    MenuItemId = 16,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 26,
                    OrderId = 13,
                    MenuItemId = 1,
                    Quantity = 2,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 27,
                    OrderId = 14,
                    MenuItemId = 9,
                    Quantity = 2,
                    Subtotal = 10,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 28,
                    OrderId = 14,
                    MenuItemId = 15,
                    Quantity = 1,
                    Subtotal = 5,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 29,
                    OrderId = 15,
                    MenuItemId = 8,
                    Quantity = 2,
                    Subtotal = 6,
                    Valid = true
                },
                new OrderItem
                {
                    Id = 30,
                    OrderId = 15,
                    MenuItemId = 7,
                    Quantity = 7,
                    Subtotal = 14,
                    Valid = true
                }
            );
        }
    }
}




