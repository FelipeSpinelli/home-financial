using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(Revenue))]
    public class RevenueMapping
    {
        public string Id { get;  set; }
        public string OwnerId { get;  set; }
        public string AccountId { get;  set; }
        public string ReferenceName { get;  set; }
        public double Amount { get;  set; }
        public string CreditDayType { get;  set; }
        public int CreditDayValue { get;  set; }
    }
}
