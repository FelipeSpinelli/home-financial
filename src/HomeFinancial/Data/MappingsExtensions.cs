using HomeFinancial.Data.Mappings;
using ServiceStack.OrmLite;
using System.Data;

namespace HomeFinancial.Data
{
    public static class MappingsExtensions
    {
        public static IDbConnection ExecuteDatabaseMappings(this IDbConnection dbConnection)
        {
            dbConnection
                .CreateTable<MemberMapping>()
                .CreateTable<AccountMapping>()
                .CreateTable<BuyerMapping>()
                .CreateTable<CreditCardMapping>()
                .CreateTable<CreditCardInvoiceMapping>()
                .CreateTable<CreditCardInvoiceDetailMapping>()
                .CreateTable<RevenueMapping>(); ;

            return dbConnection;
        }

        public static IDbConnection CreateTable<T>(this IDbConnection dbConnection) where T : class
        {
            dbConnection.CreateTableIfNotExists<T>();
            return dbConnection;
        }
    }
}
