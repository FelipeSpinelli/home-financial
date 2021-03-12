using Dapper;
using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Repositories;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinancial.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public AccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Insert(Account account)
        {
            const string QUERY = @"INSERT INTO public.account(id, owner_id, reference_name, bank, agency, ""number"", is_saving, balance)
                          VALUES(@Id, @OwnerId, @ReferenceName, @Bank, @Agency, @Number, @IsSaving, @Balance);";

            await _dbConnection.ExecuteAsync(QUERY, account);
        }

        public async Task Update(string id, Account account)
        {
            const string QUERY = @"UPDATE public.account SET 
                          owner_id = @OwnerId, reference_name = @ReferenceName, 
                          bank = @Bank, agency = @Agency, `number` = @Number, is_saving = @IsSaving, balance = @Balance
                          WHERE id = @Id";

            await _dbConnection.ExecuteAsync(QUERY, account);
        }

        public async Task<Account> GetById(string id)
        {
            const string QUERY = @"SELECT id as ""Id"", owner_id as ""OwnerId"", reference_name as ""ReferenceName"", 
                                   bank as ""Bank"", agency as ""Agency"", ""number"" as ""Number"", is_saving as ""IsSaving"", 
                                   balance as ""Balance"" FROM account WHERE id = @id;";
            var results = await _dbConnection.QueryAsync<Account>(QUERY, new { id = id });
            return results.FirstOrDefault();
        }
    }
}
