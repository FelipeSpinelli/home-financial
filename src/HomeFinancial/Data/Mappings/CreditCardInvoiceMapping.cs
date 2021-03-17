using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(CreditCardInvoice))]
    public class CreditCardInvoiceMapping
    {
        public string Id { get;  set; }
        public string CreditCardId { get;  set; }
        public ushort Reference { get;  set; }
        public string Status { get;  set; }
        public double Amount { get;  set; }
    }
}
