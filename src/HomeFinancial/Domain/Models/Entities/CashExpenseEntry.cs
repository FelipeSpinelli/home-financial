using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public class CashExpenseEntry : Entry
    {
        public CashExpenseEntry() : base() { }

        public CashExpenseEntry
        (
            string description,
            DateTime date,
            double amount,
            Account account,
            Buyer buyer
        ) : base(description, date, date, date, EEntryType.Debit, amount, account, buyer)
        {
        }
    }
}
