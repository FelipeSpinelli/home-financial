using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Domain.Models.Entities
{
    public abstract class Entry
    {
        public string Id { get; private set; }
        public string AccountId { get; private set; }
        public string BuyerId { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime ApplicationDate { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public EEntryType Type { get; private set; }
        public double Value { get; private set; }

        public Account Account { get; private set; }
        public Buyer Buyer { get; private set; }

        protected Entry()
        {
            Id = Guid.NewGuid().ToString();
        }

        protected Entry
        (
            string description,
            DateTime date,
            DateTime applicationDate,
            DateTime? paymentDate,
            EEntryType type,
            double amount,
            Account account,
            Buyer buyer
        ) : base()
        {
            Description = description;
            Date = date;
            ApplicationDate = applicationDate;
            PaymentDate = paymentDate;
            Type = type;
            Value = type switch
            {
                EEntryType.Credit => Math.Abs(amount),
                EEntryType.Debit => Math.Abs(amount) * -1,
                _ => throw new NotImplementedException(),
            };

            AccountId = account.Id;
            Account = account;

            BuyerId = buyer.Id;
            Buyer = buyer;
        }

        public void Pay(Account account)
        {
            AccountId = account.Id;
            Account = account;
            PaymentDate = DateTime.Now;
        }
    }
}
