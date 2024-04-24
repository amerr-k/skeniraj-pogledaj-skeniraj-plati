using SPSP.Models.Request.PaymentGatewayData;
using SPSP.Models.Request.SaleInvoiceItem;

namespace SPSP.Models.Request.SaleInvoice
{
    public class SaleInvoiceCreateRequest
    {
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithVAT { get; set; }
        public decimal? VAT { get; set; }
        public PaymentGatewayDataCreateRequest? PaymentGatewayData { get; set; }
        public int? EmployeeId { get; set; }
        public int? OrderId { get; set; }
        public byte[]? PdfInvoice { get; set; }

        public bool? Processed { get; set; }
        public ICollection<SaleInvoiceItemCreateRequest>? SaleInvoiceItems { get; set; }
    }
}
