using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(CreditCardInvoice))]
    public class CreditCardInvoiceMapping
    {
        public string Id { get; private set; }
        public string CreditCardId { get; private set; }
        public ushort Reference { get; private set; }
        public string Status { get; private set; }
        public double Amount { get; private set; }
    }
}
