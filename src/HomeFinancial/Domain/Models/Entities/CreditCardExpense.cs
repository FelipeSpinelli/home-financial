using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public class CreditCardExpense : Expense
    {
        public string CreditCardId { get; private set; }
        public CreditCard CreditCard { get; private set; }

        public CreditCardExpense() : base() { }

        public CreditCardExpense
        (
            string referenceName,
            EExpenseType type,
            DateTime date,
            double amount,
            ushort installments,
            CreditCard creditCard,
            Account account,
            Buyer buyer
        ) : base(referenceName, type, EExpensePaymentType.CreditCard, date, amount, installments, account, buyer)
        {
            CreditCardId = creditCard.Id;
            CreditCard = creditCard;
        }

        public override void ApplyExpense()
        {
            for (int i = 1; i <= Installments; i++)
            {
                var invoice = CreditCard.GetInvoice(GetDateReference(i));
                invoice.AddDetail(GetEntry(i, invoice));
            };
        }

        private DateTime GetDateReference(int installment)
        {
            var installmentDateReference = Date.AddMonths(installment);
            if (installmentDateReference.Day > CreditCard.InvoiceClosingDay)
            {
                installmentDateReference.AddMonths(1);
            }

            return installmentDateReference;
        }

        private CreditCardExpenseEntry GetEntry(int installment, CreditCardInvoice invoice)
        {
            return new CreditCardExpenseEntry
            (
                $"{ReferenceName} {Date:F} {installment}/{Installments}",
                Date,
                GetDateReference(installment),
                Amount / Installments,
                Account,
                Buyer,
                invoice
            );
        }
    }
}
