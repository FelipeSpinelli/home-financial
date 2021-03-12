using HomeFinancial.Domain.Models.Entities;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Repositories
{
    public interface ICreditCardRepository
    {
        Task Insert(CreditCard creditCard);
        Task Update(string id, CreditCard creditCard);
    }
}
