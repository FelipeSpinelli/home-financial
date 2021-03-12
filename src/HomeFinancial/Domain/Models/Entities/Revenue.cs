using HomeFinancial.Domain.Models.Enums;
using System;
using System.Diagnostics.CodeAnalysis;

namespace HomeFinancial.Domain.Models.Entities
{
    public class Revenue : IEquatable<Revenue>
    {
        public string Id { get; private set; }
        public string AccountId { get; private set; }
        public string ReferenceName { get; private set; }
        public double Amount { get; private set; }
        public ERevenueCreditDayType CreditDayType { get; private set; }
        public int CreditDayValue { get; private set; }

        public Account Target { get; private set; }

        public Revenue()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Revenue
        (
            string referenceName,
            double amount,
            ERevenueCreditDayType creditDayType,
            int creditDayValue,
            Account target
        ) : this()
        {
            ReferenceName = referenceName;
            Amount = amount;
            CreditDayType = creditDayType;
            CreditDayValue = creditDayValue;

            if (target == null)
            {
                return;
            }

            SetTarget(target);
        }

        public void SetTarget(Account target)
        {
            AccountId = target.Id;
            Target = target;
        }

        public void UpdateAmount(double amount) => Amount = amount;
        public void UpdateCreditDay(ERevenueCreditDayType type, int value)
        {
            CreditDayType = type;
            CreditDayValue = value;
        }

        public bool IsCreditDay(DateTime? dateToCheck = default)
        {
            dateToCheck ??= DateTime.Now;
            return CreditDayType switch
            {
                ERevenueCreditDayType.Fixed => dateToCheck.Value.Day == CreditDayValue,
                ERevenueCreditDayType.OrdinalBusinessDay => IsOrdinalBusinessDay(dateToCheck.Value, CreditDayValue),
                ERevenueCreditDayType.RelativeToMonthLastBusinessDay => IsRelativeToMonthLastBusinessDay(dateToCheck.Value, CreditDayValue),
                _ => throw new NotImplementedException(),
            };
        }

        public bool Equals([AllowNull] Revenue other)
        {
            return other == null || other.Id == Id;
        }

        private static bool IsOrdinalBusinessDay(DateTime dateToCheck, int order)
        {
            var currentOrder = 0;
            for (int i = 1; i <= DateTime.DaysInMonth(dateToCheck.Year, dateToCheck.Month); i++)
            {
                var date = new DateTime(dateToCheck.Year, dateToCheck.Month, i);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                ++currentOrder;

                if (currentOrder == order)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsRelativeToMonthLastBusinessDay(DateTime dateToCheck, int reference)
        {
            for (int i = DateTime.DaysInMonth(dateToCheck.Year, dateToCheck.Month); i >= 1; i--)
            {
                var date = new DateTime(dateToCheck.Year, dateToCheck.Month, i);
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                --reference;

                if (reference > 0)
                {
                    continue;
                }

                return date.Date == dateToCheck.Date;
            }

            return false;
        }
    }
}
