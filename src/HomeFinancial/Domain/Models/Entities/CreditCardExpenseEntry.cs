using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public class CreditCardExpenseEntry : Entry
    {
        public string CreditCardInvoiceId { get; private set; }
        
        public CreditCardInvoice CreditCardInvoice { get; private set; }

        public CreditCardExpenseEntry() : base() { }

        public CreditCardExpenseEntry
        (
            string description,
            DateTime date,
            DateTime applicationDate,
            double amount,
            Account account,
            Buyer buyer,
            CreditCardInvoice creditCardInvoice
        ) : base(description, date, applicationDate, null, EEntryType.Debit, amount, account, buyer)
        {
            CreditCardInvoiceId = creditCardInvoice.Id;
            CreditCardInvoice = creditCardInvoice;
        }
    }
}
