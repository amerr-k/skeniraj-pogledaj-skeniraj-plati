using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSP.Models.Enums
{
    public enum OrderStatusEnum
    {
        ACTIVE,
        COMPLETED,
        CANCELED
    }

    public static class OrderStatusEnumExtension
    {
        public static string GetValue(this OrderStatusEnum status)
        {
            switch (status)
            {
                case OrderStatusEnum.ACTIVE:
                    return "AKTIVNO";
                case OrderStatusEnum.COMPLETED:
                    return "ZAVRŠENO";
                case OrderStatusEnum.CANCELED:
                    return "OTKAZANO";
                default:
                    throw new ArgumentException("Invalid OrderStatus");
            }
        }
    }
}
