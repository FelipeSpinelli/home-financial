using Dapper;
using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinancial.Data.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IDbConnection _dbConnection;

        public MemberRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            const string QUERY = @"SELECT id as ""Id"", name as ""Name"" FROM member";
            var results = await _dbConnection.QueryAsync<Member>(QUERY);
            return results;
        }

        public async Task<Member> GetById(string id)
        {
            const string QUERY = @"
                SELECT 
	                member.id as ""Id"", member.name as ""Name"", account.id as ""Id"", account.owner_id as ""OwnerId"", 
	                account.reference_name as ""ReferenceName"", account.bank as ""Bank"", account.agency as ""Agency"", 
	                account.""number"" as ""Number"", account.is_saving as ""IsSaving"", account.balance as ""Balance"",
	                credit_card.id as ""Id"", credit_card.holder_id as ""HolderId"", credit_card.reference_name as ""ReferenceName"", 
	                credit_card.brand as ""Brand"", credit_card.color as ""Color"", credit_card.holder_name as ""HolderName"", credit_card.""number"" as ""Number"", 
	                credit_card.expiration_date as ""ExpirationDate"", credit_card.verification_code as ""VerificationCode"", 
	                credit_card.due_day as ""DueDay"", credit_card.invoice_closing_day as ""InvoiceClosingDay"", credit_card.""limit"" as ""Limit"",
	                revenue.id as ""Id"", revenue.owner_id as ""OwnerId"", revenue.account_id as ""AccountId"", revenue.reference_name as ""ReferenceName"", 
	                revenue.amount as ""Amount"", revenue.credit_day_type as ""CreditDayType"", revenue.credit_day_value as ""CreditDayValue""
                FROM member 
                LEFT JOIN account ON account.owner_id = member.id 
                LEFT JOIN credit_card ON credit_card.holder_id = member.id 
                LEFT JOIN revenue ON revenue.account_id = account.id 
                WHERE member.id = @id";

            Member member = null;
            var result = await _dbConnection.QueryAsync<Member, Account, CreditCard, Revenue, Member>
            (
                QUERY,
                (m, a, cc, r) =>
                {
                    member ??= m;
                    if (a != null)
                    {
                        member.AddAccount(a);
                    }

                    if (cc != null)
                    {
                        member.AddCreditCard(cc);
                    }

                    if (r != null)
                    {
                        member.Accounts.FirstOrDefault(x => x.Id == r.AccountId).AddRevenue(r);
                    }

                    return m;
                },
                param: new { id = id }
            );
            return result.FirstOrDefault();
        }

        public async Task Insert(Member member)
        {
            const string QUERY = "INSERT INTO public.member(id, name) VALUES(@Id, @Name);";
            await _dbConnection.ExecuteAsync(QUERY, member);
        }

        public async Task Update(string id, Member member)
        {
            const string QUERY = "UPDATE public.member set name = @Name WHERE id = @Id;";
            await _dbConnection.ExecuteAsync(QUERY, member);
        }
    }
}
