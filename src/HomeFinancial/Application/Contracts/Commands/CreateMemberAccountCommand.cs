using HomeFinancial.Domain.Models.Entities;

namespace HomeFinancial.Application.Contracts.Commands
{
    public class CreateMemberAccountCommand
    {
        public string MemberId { get; set; }
        public string ReferenceName { get; set; }
        public string Bank { get; set; }
        public string Agency { get; set; }
        public string Number { get; set; }
        public bool IsSaving { get; set; }
        public double Balance { get; set; }

        public static explicit operator Account(CreateMemberAccountCommand createMemberAccountCommand)
        {
            return new Account
            (
                createMemberAccountCommand.ReferenceName,
                createMemberAccountCommand.Bank,
                createMemberAccountCommand.Agency,
                createMemberAccountCommand.Number,
                createMemberAccountCommand.IsSaving,
                createMemberAccountCommand.Balance,
                null
            );
        }
    }
}
