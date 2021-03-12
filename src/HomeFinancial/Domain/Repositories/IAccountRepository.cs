using HomeFinancial.Domain.Models.Entities;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task Insert(Account account);
        Task Update(string id, Account account);
        Task<Account> GetById(string id);
    }
}
