using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace HomeFinancial.Domain.Services.Implementations
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IBuyerRepository _buyerRepository;

        public ExpenseService(IExpenseRepository expenseRepository, IBuyerRepository buyerRepository)
        {
            _expenseRepository = expenseRepository;
            _buyerRepository = buyerRepository;
        }

        public async Task Insert(Expense expense)
        {
            await InsertBuyerIfNecessary(expense);

            switch (expense)
            {
                case CreditCardExpense creditCardExpense:
                    await _expenseRepository.Insert(creditCardExpense);
                    break;
                case CashExpense cashExpense:
                    await _expenseRepository.Insert(cashExpense);
                    break;
                default:
                    throw new ArgumentException($"Unexpected expense type {expense.PaymentType}!", nameof(expense));
            }
        }

        private async Task InsertBuyerIfNecessary(Expense expense)
        {
            var buyer = await _buyerRepository.GetById(expense.BuyerId);
            if (buyer != null)
            {
                expense.SetBuyer(buyer);
                return;
            }

            await _buyerRepository.Insert(expense.Buyer);
        }
    }
}
