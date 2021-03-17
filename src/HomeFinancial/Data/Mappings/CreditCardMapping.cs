using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(CreditCard))]
    public class CreditCardMapping
    {
        public string Id { get; private set; }
        public string HolderId { get; private set; }
        public string ReferenceName { get; private set; }
        public string Brand { get; private set; }
        public string Color { get; private set; }
        public string HolderName { get; private set; }
        public string Number { get; private set; }
        public string ExpirationDate { get; private set; }
        public string VerificationCode { get; private set; }
        public int DueDay { get; private set; }
        public int InvoiceClosingDay { get; private set; }
        public double Limit { get; private set; }
    }
}
