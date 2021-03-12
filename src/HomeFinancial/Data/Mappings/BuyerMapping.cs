using HomeFinancial.Domain.Models.Entities;
using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias(nameof(Buyer))]
    public class BuyerMapping
    {
        public string Id { get; set; }
        public string MemberId { get; set; }
        public string Name { get; set; }
    }
}
