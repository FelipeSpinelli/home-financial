using HomeFinancial.Application.Contracts.Responses;
using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeFinancial.Application.Contracts.Commands
{
    public class CreateMemberRevenueCommand
    {
        private static readonly IDictionary<string, string> _revenueCreditDayTypes = new Dictionary<string, string>
        {
            { "Fixed", "Dia do mês" },
            { "OrdinalBusinessDay", "Dia útil do mês" },
            { "RelativeToMonthLastBusinessDay", "Dias utéis antes do fim do mês" }
        };
        public static readonly string[] RevenueCreditDayTypes = _revenueCreditDayTypes.Select(x => x.Value).ToArray();

        public MemberDetailedAccountResponse Account { get; set; }
        public string ReferenceName { get; set; }
        public double Amount { get; set; }
        public string CreditDayType { get; set; } = RevenueCreditDayTypes[0];
        public ushort CreditDayValue { get; set; }

        public static explicit operator Revenue(CreateMemberRevenueCommand createMemberRevenueCommand)
        {
            return new Revenue
            (
                createMemberRevenueCommand.ReferenceName,
                createMemberRevenueCommand.Amount,
                Enum.Parse<ERevenueCreditDayType>(createMemberRevenueCommand.CreditDayType),
                createMemberRevenueCommand.CreditDayValue,
                null
            );
        }

        public static string GetRevenueCreditDayKey(string revenueCreditDayTypeValue)
        {
            return _revenueCreditDayTypes.FirstOrDefault(x => x.Value == revenueCreditDayTypeValue).Key;
        }

        public static string GetRevenueCreditDayValue(string revenueCreditDayTypeKey)
        {
            return _revenueCreditDayTypes.FirstOrDefault(x => x.Key == revenueCreditDayTypeKey).Value;
        }
    }
}
