using HomeFinancial.Domain.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Services
{
    public interface IMemberService
    {
        Task Upsert(Member member, string id = default);
        Task AddAccount(string memberId, Account account);
        Task AddCreditCard(string id, CreditCard creditCard);
        Task<IEnumerable<Member>> GetAll();
        Task<Member> GetById(string id);
        Task AddRevenue(string accountId, Revenue revenue);
    }
}
