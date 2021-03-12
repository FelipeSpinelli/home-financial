using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(Revenue))]
    public class RevenueMapping
    {
        public string Id { get; private set; }
        public string OwnerId { get; private set; }
        public string AccountId { get; private set; }
        public string ReferenceName { get; private set; }
        public double Amount { get; private set; }
        public string CreditDayType { get; private set; }
        public int CreditDayValue { get; private set; }
    }
}
