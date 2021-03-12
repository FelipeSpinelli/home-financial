using HomeFinancial.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Repositories
{
    public interface IMemberRepository
    {
        Task Insert(Member member);
        Task Update(string id, Member member);
        Task<IEnumerable<Member>> GetAll();
        Task<Member> GetById(string id);
    }
}
