using HomeFinancial.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HomeFinancial.Domain.Models.Entities
{
    public class CreditCard : IEquatable<CreditCard>
    {
        private readonly List<CreditCardInvoice> _invoices;

        public string Id { get; private set; }
        public string HolderId { get; private set; }
        public string ReferenceName { get; private set; }
        public EBrand Brand { get; private set; }
        public string HolderName { get; private set; }
        public string Number { get; private set; }
        public string ExpirationDate { get; private set; }
        public string VerificationCode { get; private set; }
        public int DueDay { get; private set; }
        public int InvoiceClosingDay { get; private set; }
        public double Limit { get; private set; }
        public CreditCardInvoice CurrentInvoice => _invoices.FirstOrDefault(x => x.Status == ECreditCardInvoiceStatus.Opened);

        public Member Holder { get; private set; }
        public IReadOnlyList<CreditCardInvoice> Invoices => _invoices;

        public CreditCard() : this(null)
        {
        }

        public CreditCard
        (
            string referenceName,
            EBrand brand,
            string holderName,
            string number,
            string expirationDate,
            string verificationCode,
            int dueDay,
            int invoiceClosingDay,
            double limit,
            Member holder
        ) : this()
        {
            ReferenceName = referenceName;
            Brand = brand;
            HolderName = holderName;
            Number = number;
            ExpirationDate = expirationDate;
            VerificationCode = verificationCode;
            DueDay = dueDay;
            InvoiceClosingDay = invoiceClosingDay;
            Limit = limit;

            if (holder == null)
            {
                return;
            }

            SetHolder(holder);
        }

        public void SetHolder(Member holder)
        {
            HolderId = holder.Id;
            Holder = holder;
        }

        private CreditCard(IEnumerable<CreditCardInvoice> invoices)
        {
            Id = Guid.NewGuid().ToString();
            _invoices = invoices?.ToList() ?? new List<CreditCardInvoice>();
        }

        public void CloseCurrentInvoice() => CurrentInvoice.Close();

        public void OpenInvoice()
        {
            var invoice = _invoices.OrderBy(i => i.Reference).FirstOrDefault(x => x.Status == ECreditCardInvoiceStatus.NotStarted);
            invoice.Open();
        }

        public CreditCardInvoice PayInvoice(DateTime referenceDate)
        {
            var invoice = GetInvoice(referenceDate) ?? throw new NullReferenceException("Invoice not found!");

            invoice.MarkAsPaid();
            return invoice;
        }

        public CreditCardInvoice GetInvoice(DateTime referenceDate)
        {
            var invoice = _invoices.FirstOrDefault(i => i.Reference == CreditCardInvoice.GetReferenceFromDate(referenceDate));

            if (invoice != null)
            {
                return invoice;
            }

            invoice = new CreditCardInvoice(this, referenceDate);
            _invoices.Add(invoice);
            return invoice;
        }

        public bool Equals([AllowNull] CreditCard other)
        {
            return other == null || other.Id == Id;
        }
    }
}
