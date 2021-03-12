using HomeFinancial.Application.Services;
using HomeFinancial.Application.Services.Implementations;
using HomeFinancial.Data;
using HomeFinancial.Data.Repositories;
using HomeFinancial.Domain.Repositories;
using HomeFinancial.Domain.Services;
using HomeFinancial.Domain.Services.Implementations;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Net.Http;

namespace HomeFinancial
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMatBlazor();
            services
                .AddScoped<HttpClient>()
                .AddScoped<IMemberAppService, MemberAppService>()
                .AddScoped<IMemberService, MemberService>()
                .AddScoped<IMemberRepository, MemberRepository>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<ICreditCardRepository, CreditCardRepository>()
                .AddScoped<IRevenueRepository, RevenueRepository>()
                .AddSingleton<IDbConnectionFactory>(provider => new OrmLiteConnectionFactory("Server=127.0.0.1;Port=5432;Database=home_financial;User Id=postgres;Password=postgres;", PostgreSqlDialect.Provider))
                .AddScoped(provider =>
                {
                    var dbConnectionFactory = provider.GetRequiredService<IDbConnectionFactory>();
                    var dbConnection = dbConnectionFactory.CreateDbConnection();
                    dbConnection.Open();
                    dbConnection.ExecuteDatabaseMappings();
                    return dbConnection;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
