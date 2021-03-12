using HomeFinancial.Domain.Models.Entities;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Repositories
{
    public interface IRevenueRepository
    {
        Task Insert(Revenue revenue);
        Task Update(string id, Revenue revenue);
    }
}
