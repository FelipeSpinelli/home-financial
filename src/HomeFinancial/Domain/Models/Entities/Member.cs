using System;
using System.Collections.Generic;

namespace HomeFinancial.Domain.Models.Entities
{
    public class Member
    {
        private readonly List<Account> _accounts = new List<Account>();
        private readonly List<CreditCard> _creditCards = new List<CreditCard>();

        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Name { get; private set; }
        public IReadOnlyList<Account> Accounts => _accounts;
        public IReadOnlyList<CreditCard> CreditCards => _creditCards;

        public Member() { }

        public Member(string name)
        {
            Name = name;
        }

        public void AddAccount(Account account)
        {
            if (_accounts.Contains(account))
            {
                return;
            }

            _accounts.Add(account);
        }

        public void AddCreditCard(CreditCard creditCard)
        {
            if (_creditCards.Contains(creditCard))
            {
                return;
            }

            _creditCards.Add(creditCard);
        }

        public void ProcessInvoicesCycles()
        {
            foreach (var creditCard in _creditCards)
            {
                creditCard.CloseCurrentInvoice();
                creditCard.OpenInvoice();
            }
        }

        public void PayCreditCardInvoice
        (
            CreditCard creditCard,
            Account account,
            DateTime referenceDate
        )
        {
            var invoice = creditCard.PayInvoice(referenceDate);
            foreach (var invoiceDetail in invoice.Details)
            {
                account.AddEntry(invoiceDetail.Entry);
            }
        }
    }
}
