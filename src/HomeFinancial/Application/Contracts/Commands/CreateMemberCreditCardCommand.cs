using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Models.Enums;
using System;

namespace HomeFinancial.Application.Contracts.Commands
{
    public class CreateMemberCreditCardCommand
    {
        public static readonly string[] Brands = new[] { "MasterCard", "Visa" };

        public string HolderId { get; set; }
        public string ReferenceName { get; set; }
        public string Brand { get; set; }
        public string HolderName { get; set; }
        public string Number { get; set; }
        public string ExpirationDate { get; set; }
        public string VerificationCode { get; set; }
        public int DueDay { get; set; }
        public int InvoiceClosingDay { get; set; }
        public double Limit { get; set; }

        public static explicit operator CreditCard(CreateMemberCreditCardCommand createMemberCreditCardCommand)
        {
            return new CreditCard
            (
                createMemberCreditCardCommand.ReferenceName,
                Enum.Parse<EBrand>(createMemberCreditCardCommand.Brand),
                createMemberCreditCardCommand.HolderName,
                createMemberCreditCardCommand.Number,
                createMemberCreditCardCommand.ExpirationDate,
                createMemberCreditCardCommand.VerificationCode,
                createMemberCreditCardCommand.DueDay,
                createMemberCreditCardCommand.InvoiceClosingDay,
                createMemberCreditCardCommand.Limit,
                null
            );
        }
    }
}
