using HomeFinancial.Domain.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HomeFinancial.Application.Contracts.Responses
{
    public class MemberDetailedResponse
    {
        private static readonly MemberDetailedResponse _empty = new MemberDetailedResponse { Name = "-" };

        public string Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<MemberDetailedAccountResponse> Accounts { get; set; } = new List<MemberDetailedAccountResponse>(0);
        public IEnumerable<MemberDetailedCreditCardResponse> CreditCards { get; set; } = new List<MemberDetailedCreditCardResponse>(0);
        public IEnumerable<MemberDetailedRevenueResponse> Revenues { get; set; } = new List<MemberDetailedRevenueResponse>(0);

        public static explicit operator MemberDetailedResponse(Member member)
        {
            return new MemberDetailedResponse
            {
                Id = member.Id,
                Name = member.Name,
                Accounts = member.Accounts?
                    .Select(account => (MemberDetailedAccountResponse)account) ?? new List<MemberDetailedAccountResponse>(0),
                CreditCards = member.CreditCards?
                    .Select(creditCard => (MemberDetailedCreditCardResponse)creditCard) ?? new List<MemberDetailedCreditCardResponse>(0),
                Revenues = member.Accounts?
                    .SelectMany(x => x.Revenues ?? new List<Revenue>(0))
                    .Select(revenue => (MemberDetailedRevenueResponse)revenue) ?? new List<MemberDetailedRevenueResponse>(0),
            };
        }


        public static MemberDetailedResponse Empty => _empty;
    }

    public class MemberDetailedAccountResponse
    {
        public string Id { get; set; }
        public string ReferenceName { get; set; }
        public bool IsSaving { get; set; }
        public double Balance { get; set; }

        public override string ToString() => ReferenceName;

        public static explicit operator MemberDetailedAccountResponse(Account account)
        {
            return new MemberDetailedAccountResponse
            {
                Id = account.Id,
                ReferenceName = account.ReferenceName,
                IsSaving = account.IsSaving,
                Balance = account.Balance
            };
        }
    }

    public class MemberDetailedCreditCardResponse
    {
        public string Id { get; set; }
        public string ReferenceName { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public double Limit { get; set; }
        public double AvailableAmount { get; set; }
        public double AvailableRate => AvailableAmount / Limit;

        public static explicit operator MemberDetailedCreditCardResponse(CreditCard creditCard)
        {
            return new MemberDetailedCreditCardResponse
            {
                Id = creditCard.Id,
                ReferenceName = creditCard.ReferenceName,
                Brand = creditCard.Brand.ToString(),
                Color = creditCard.Brand switch
                {
                    Domain.Models.Enums.EBrand.MasterCard => "#ebb134",
                    Domain.Models.Enums.EBrand.Visa => "#3462eb",
                    _ => "#ffffff"
                },
                Limit = creditCard.Limit,
                AvailableAmount = creditCard.Limit - (creditCard.CurrentInvoice?.Amount ?? 0)
            };
        }
    }
    public class MemberDetailedRevenueResponse
    {
        public string Id { get; set; }
        public string ReferenceName { get; set; }
        public double Amount { get; set; }

        public static explicit operator MemberDetailedRevenueResponse(Revenue revenue)
        {
            return new MemberDetailedRevenueResponse
            {
                Id = revenue.Id,
                ReferenceName = revenue.ReferenceName,
                Amount = revenue.Amount
            };
        }
    }
}
