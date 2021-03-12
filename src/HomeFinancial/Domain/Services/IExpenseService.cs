using HomeFinancial.Domain.Models.Entities;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Services
{
    public interface IExpenseService
    {
        Task Insert(Expense expense);
    }
}
