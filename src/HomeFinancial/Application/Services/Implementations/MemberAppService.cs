using HomeFinancial.Application.Contracts.Commands;
using HomeFinancial.Application.Contracts.Responses;
using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinancial.Application.Services.Implementations
{
    public class MemberAppService : IMemberAppService
    {
        private readonly IMemberService _memberService;

        public MemberAppService(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<(bool IsValid, IEnumerable<string> Errors)> Create(CreateMemberCommand createMemberCommand)
        {
            var validation = await CreateMemberCommandValidator.Instance.ValidateAsync(createMemberCommand);
            if (!validation.IsValid)
            {
                return (validation.IsValid, validation.Errors.Select(x => x.ErrorMessage));
            }

            var member = new Member(createMemberCommand.Name);

            await _memberService.Upsert(member);
            return (true, null);
        }

        public async Task AddAccount(CreateMemberAccountCommand createMemberAccountCommand)
        {
            var account = (Account)createMemberAccountCommand;
            await _memberService.AddAccount(createMemberAccountCommand.MemberId, account);
        }

        public async Task AddCreditCard(CreateMemberCreditCardCommand createMemberCreditCardCommand)
        {
            var creditCard = (CreditCard)createMemberCreditCardCommand;
            await _memberService.AddCreditCard(createMemberCreditCardCommand.HolderId, creditCard);
        }

        public async Task AddRevenue(CreateMemberRevenueCommand createMemberRevenueCommand)
        {
            var revenue = (Revenue)createMemberRevenueCommand;
            await _memberService.AddRevenue(createMemberRevenueCommand.Account.Id, revenue);
        }

        public async Task<IEnumerable<GetAllMemberResponse>> GetAll()
        {
            var members = await _memberService.GetAll();
            return members.Select(x => (GetAllMemberResponse)x);
        }

        public async Task<MemberDetailedResponse> GetById(string id)
        {
            return (MemberDetailedResponse)await _memberService.GetById(id);
        }
    }
}
