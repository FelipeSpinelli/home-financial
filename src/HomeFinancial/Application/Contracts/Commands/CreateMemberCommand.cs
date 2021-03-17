using FluentValidation;

namespace HomeFinancial.Application.Contracts.Commands
{
    public class CreateMemberCommand
    {
        public string Name { get; set; }
    }

    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public static readonly CreateMemberCommandValidator Instance = new CreateMemberCommandValidator();

        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Informe o nome!");
        }
    }
}
