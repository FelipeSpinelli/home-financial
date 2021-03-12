using HomeFinancial.Domain.Models.Entities;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Repositories
{
    public interface IExpenseRepository
    {
        Task Insert(CreditCardExpense creditCardExpense);
        Task Insert(CashExpense cashExpense);
    }
}
