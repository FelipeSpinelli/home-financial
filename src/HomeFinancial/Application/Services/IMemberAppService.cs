using HomeFinancial.Application.Contracts.Commands;
using HomeFinancial.Application.Contracts.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFinancial.Application.Services
{
    public interface IMemberAppService
    {
        Task Create(CreateMemberCommand createMemberCommand);
        Task AddAccount(CreateMemberAccountCommand createMemberAccountCommand);
        Task AddCreditCard(CreateMemberCreditCardCommand createMemberCreditCardCommand);
        Task AddRevenue(CreateMemberRevenueCommand createMemberRevenueCommand);
        Task<IEnumerable<GetAllMemberResponse>> GetAll();
        Task<MemberDetailedResponse> GetById(string id);
    }
}
