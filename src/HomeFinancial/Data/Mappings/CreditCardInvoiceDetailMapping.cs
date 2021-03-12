using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(CreditCardInvoiceDetail))]
    public class CreditCardInvoiceDetailMapping
    {
        public string Id { get; private set; }
        public string InvoiceId { get; private set; }
        public string EntryId { get; private set; }
        public string ReferenceName { get; private set; }
        public double Amount { get; private set; }
    }
}
