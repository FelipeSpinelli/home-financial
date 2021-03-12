using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public class RevenueEntry : Entry
    {
        public RevenueEntry() : base() { }

        public RevenueEntry
        (
            Revenue revenue,
            Account account
        ) : base(revenue.ReferenceName, DateTime.Now, DateTime.Now, DateTime.Now, EEntryType.Credit, revenue.Amount, account, null)
        {

        }
    }
}
