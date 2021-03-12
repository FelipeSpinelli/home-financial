using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias("member")]
    public class MemberMapping
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
