using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HomeFinancial.Domain.Models.Entities
{
    public class Account : IEquatable<Account>
    {
        private readonly List<Entry> _entries;
        private readonly List<Revenue> _revenues;

        public string Id { get; private set; }
        public string OwnerId { get; private set; }
        public string ReferenceName { get; private set; }
        public string Bank { get; private set; }
        public string Agency { get; private set; }
        public string Number { get; private set; }
        public bool IsSaving { get; private set; }
        public double Balance { get; private set; }
        public IReadOnlyList<Entry> Entries => _entries;
        public IReadOnlyList<Revenue> Revenues => _revenues;

        public Member Owner { get; private set; }

        public Account()
        {
            Id = Guid.NewGuid().ToString();
            _entries = new List<Entry>();
            _revenues = new List<Revenue>();
        }

        public Account
        (
            string referenceName,
            string bank,
            string agency,
            string number,
            bool isSaving,
            double balance,
            Member owner
        ) : this()
        {
            ReferenceName = referenceName;
            Bank = bank;
            Agency = agency;
            Number = number;
            IsSaving = isSaving;
            Balance = balance;

            if (owner == null)
            {
                return;
            }

            SetOwner(owner);
        }

        public void AddRevenue(Revenue revenue)
        {
            if (revenue.AccountId != Id)
            {
                throw new ArgumentException("The given revenue doesn't belong to this account!", nameof(revenue));
            }

            if (_revenues.Contains(revenue))
            {
                return;
            }

            _revenues.Add(revenue);
        }

        public void ApplyRevenues()
        {
            foreach (var revenue in _revenues.Where(r => r.IsCreditDay()))
            {
                AddEntry(new RevenueEntry(revenue, this));
            }
        }

        public virtual void AddEntry(Entry entry)
        {
            entry.Pay(this);
            _entries.Add(entry);
            Balance += entry.Value;
        }

        public void SetOwner(Member owner)
        {
            Owner = owner;
            OwnerId = owner.Id;
        }

        public bool Equals([AllowNull] Account other)
        {
            return other == null || other.Id == Id;
        }
    }
}
