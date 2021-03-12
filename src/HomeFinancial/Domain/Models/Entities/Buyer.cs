using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public class Buyer
    {
        public string Id { get; private set; }
        public string MemberId { get; private set; }
        public string Name { get; private set; }

        public Member Member { get; private set; }

        public Buyer()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Buyer(string name, Member member)
        {
            Name = name;
            SetMember(member);
        }

        private void SetMember(Member member)
        {
            MemberId = member.Id;
            Member = member;
        }
    }
}
