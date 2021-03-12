using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public class CashExpense : Expense
    {
        public CashExpense() : base() { }

        public CashExpense
        (
            string referenceName,
            EExpenseType type,
            DateTime date,
            double amount,
            Account account,
            Buyer buyer
        ) : base(referenceName, type, EExpensePaymentType.Cash, date, amount, 0, account, buyer)
        {
        }

        public override void ApplyExpense()
        {
            var entry = new CashExpenseEntry(ToString(), Date, Amount, Account, Buyer);
            Account.AddEntry(entry);
        }

        public override string ToString() => $"{ReferenceName} {Date:F}";
    }
}
