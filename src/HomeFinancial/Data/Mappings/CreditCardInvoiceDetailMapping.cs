using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(CreditCardInvoiceDetail))]
    public class CreditCardInvoiceDetailMapping
    {
        public string Id { get; set; }
        public string InvoiceId { get;  set; }
        public string EntryId { get;  set; }
        public string ReferenceName { get;  set; }
        public double Amount { get;  set; }
    }
}
