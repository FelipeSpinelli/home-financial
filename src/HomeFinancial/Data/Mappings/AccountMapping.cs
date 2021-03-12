using ServiceStack.DataAnnotations;

namespace HomeFinancial.Data.Mappings
{
    [Alias("account")]
    public class AccountMapping
    {
        public string Id { get;  set; }
        public string OwnerId { get;  set; }
        public string ReferenceName { get;  set; }
        public string Bank { get;  set; }
        public string Agency { get;  set; }
        public string Number { get;  set; }
        public bool IsSaving { get;  set; }
        public double Balance { get;  set; }
    }
}
