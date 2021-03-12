using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public abstract class Expense
    {
        public string Id { get; private set; }
        public string AccountId { get; private set; }
        public string BuyerId { get; private set; }
        public string ReferenceName { get; private set; }
        public EExpenseType Type { get; private set; }
        public EExpensePaymentType PaymentType { get; private set; }
        public DateTime Date { get; private set; }
        public double Amount { get; private set; }
        public ushort Installments { get; private set; }

        public Buyer Buyer { get; private set; }
        public Account Account { get; private set; }

        public Expense()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Expense
        (
            string referenceName,
            EExpenseType type,
            EExpensePaymentType paymentType,
            DateTime date,
            double amount,
            ushort installments,
            Account account,
            Buyer buyer
        ) : this()
        {
            ReferenceName = referenceName;
            Type = type;
            PaymentType = paymentType;
            Date = date;
            Amount = amount;
            Installments = installments;

            AccountId = account.Id;
            Account = account;

            SetBuyer(buyer);
        }

        public void SetBuyer(Buyer buyer)
        {
            Buyer = buyer ?? throw new NullReferenceException("Invalid Buyer!");
            BuyerId = buyer.Id;
        }

        public abstract void ApplyExpense();
    }
}
