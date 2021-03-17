using Dapper;
using HomeFinancial.Domain.Models.Entities;
using HomeFinancial.Domain.Repositories;
using System.Data;
using System.Threading.Tasks;

namespace HomeFinancial.Data.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly IDbConnection _dbConnection;

        public CreditCardRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Insert(CreditCard creditCard)
        {
            var query = @"INSERT INTO public.credit_card(id, holder_id, reference_name, brand, color, holder_name, ""number"", expiration_date, verification_code, due_day, invoice_closing_day, ""limit"")
                          VALUES(@Id, @HolderId, @ReferenceName, @Brand, @Color, @HolderName, @Number, @ExpirationDate, @VerificationCode, @DueDay, @InvoiceClosingDay, @Limit); ";

            await _dbConnection.ExecuteAsync(query, creditCard);
        }

        public async Task Update(string id, CreditCard creditCard)
        {
            var query = @"UPDATE public.credit_card
	                      SET holder_id=@HolderId, reference_name=@ReferenceName, brand=@Brand, color=@Color, holder_name=@HolderName, 
                              ""number""=@Number, expiration_date=@ExpirationDate, verification_code=@VerificationCode, 
                              due_day=@DueDay, invoice_closing_day=@InvoiceClosingDay, ""limit""=@Limit
                          WHERE id=@Id";

            await _dbConnection.ExecuteAsync(query, creditCard);
        }
    }
}
