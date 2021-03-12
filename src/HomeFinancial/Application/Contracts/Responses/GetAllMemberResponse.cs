using HomeFinancial.Domain.Models.Entities;

namespace HomeFinancial.Application.Contracts.Responses
{
    public class GetAllMemberResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static explicit operator GetAllMemberResponse(Member member)
        {
            return new GetAllMemberResponse
            {
                Id = member.Id,
                Name = member.Name
            };
        }
    }
}
