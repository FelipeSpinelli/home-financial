using HomeFinancial.Domain.Models.Entities;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Repositories
{
    public interface IBuyerRepository
    {
        Task Insert(Buyer buyer);
        Task<Buyer> GetById(string id);
    }
}
