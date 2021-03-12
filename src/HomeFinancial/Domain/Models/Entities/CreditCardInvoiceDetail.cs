using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public class CreditCardInvoiceDetail
    {
        public string Id { get; private set; }
        public string InvoiceId { get; private set; }
        public string EntryId { get; private set; }
        public string ReferenceName { get; private set; }
        public double Amount { get; private set; }

        public CreditCardInvoice Invoice { get; private set; }
        public CreditCardExpenseEntry Entry { get; private set; }

        public CreditCardInvoiceDetail()
        {
            Id = Guid.NewGuid().ToString();
        }

        public CreditCardInvoiceDetail
        (
            CreditCardInvoice invoice,
            CreditCardExpenseEntry entry
        ) : this()
        {
            ReferenceName = entry.Description;
            Amount = Math.Abs(entry.Value);

            InvoiceId = invoice.Id;
            Invoice = invoice;
            
            EntryId = entry.Id;
            Entry = entry;
        }
    }
}
