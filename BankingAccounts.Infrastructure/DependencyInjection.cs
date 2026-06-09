using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankingAccounts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<BankingDbContext>(options =>
            options.UseInMemoryDatabase("BankingAccountsDb"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
