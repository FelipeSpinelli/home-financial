using Dapper;
using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Repositories;
using System.Data;
using System.Threading.Tasks;

namespace HomeFinancial.Data.Repositories
{
    public class RevenueRepository : IRevenueRepository
    {
        private readonly IDbConnection _dbConnection;

        public RevenueRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Insert(Revenue revenue)
        {
            const string QUERY = @"INSERT INTO public.revenue(id, account_id, reference_name, amount, credit_day_type, credit_day_value)
                                   VALUES (@Id, @AccountId, @ReferenceName, @Amount, @CreditDayType, @CreditDayValue);";

            await _dbConnection.ExecuteAsync(QUERY, revenue);
        }

        public async Task Update(string id, Revenue revenue)
        {
            const string QUERY = @"UPDATE public.revenue
                                   SET owner_id=@OwnerId, account_id=@AccountId, reference_name=@ReferenceName, 
                                       amount=@Amount, credit_day_type=@CreditDayType, credit_day_value=@CreditDayValue
                                   WHERE id=@Id;";

            await _dbConnection.ExecuteAsync(QUERY, revenue);
        }
    }
}
