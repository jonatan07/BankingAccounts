using BankingAccounts.Application.Abstractions.Messaging;
using BankingAccounts.Application.Accounts.Commands;
using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Accounts.Handlers;
using BankingAccounts.Application.Accounts.Queries;
using BankingAccounts.Application.Accounts.Results;
using BankingAccounts.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BankingAccounts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<GetAllBankAccountsQuery, IReadOnlyList<BankAccountDto>>, GetAllBankAccountsQueryHandler>();
        services.AddScoped<IQueryHandler<GetBankAccountByIdQuery, OperationResult<BankAccountDto>>, GetBankAccountByIdQueryHandler>();
        services.AddScoped<ICommandHandler<CreateBankAccountCommand, OperationResult<BankAccountDto>>, CreateBankAccountCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateBankAccountCommand, OperationResult<BankAccountDto>>, UpdateBankAccountCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteBankAccountCommand, OperationResult<bool>>, DeleteBankAccountCommandHandler>();
        services.AddScoped<IBankAccountService, BankAccountService>();

        return services;
    }
}
