using HomeFinancial.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeFinancial.Domain.Models.Entities
{
    public class CreditCardInvoice
    {
        private readonly List<CreditCardInvoiceDetail> _details;

        public string Id { get; private set; }
        public string CreditCardId { get; private set; }
        public ushort Reference { get; private set; }
        public ECreditCardInvoiceStatus Status { get; private set; }
        public double Amount { get; private set; }
        public IReadOnlyList<CreditCardInvoiceDetail> Details => _details;

        public CreditCard CreditCard { get; private set; }

        public CreditCardInvoice() : this(null)
        {
        }

        public CreditCardInvoice(CreditCard creditCard, DateTime dateReference) : this()
        {
            Reference = GetReferenceFromDate(dateReference);

            CreditCardId = creditCard.Id;
            CreditCard = creditCard;
        }

        private CreditCardInvoice(IEnumerable<CreditCardInvoiceDetail> details)
        {
            Id = Guid.NewGuid().ToString();
            _details = details?.ToList() ?? new List<CreditCardInvoiceDetail>();
        }

        public void AddDetail(CreditCardExpenseEntry entry)
        {
            var detail = new CreditCardInvoiceDetail(this, entry);

            _details.Add(detail);
            Amount += _details.Sum(d => d.Amount);
        }

        public void Open() => Status = ECreditCardInvoiceStatus.Opened;

        public void Close() => Status = ECreditCardInvoiceStatus.Closed;

        public void MarkAsPaid() => Status = ECreditCardInvoiceStatus.Paid;

        public static ushort GetReferenceFromDate(DateTime dateReference) => Convert.ToUInt16($"{dateReference.Year:0000}{dateReference.Month:00)}");
    }
}
