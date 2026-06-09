using BankingAccounts.Application.Abstractions.Messaging;
using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Accounts.Mapping;
using BankingAccounts.Application.Accounts.Queries;

namespace BankingAccounts.Application.Accounts.Handlers;

public sealed class GetAllBankAccountsQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetAllBankAccountsQuery, IReadOnlyList<BankAccountDto>>
{
    public async Task<IReadOnlyList<BankAccountDto>> HandleAsync(GetAllBankAccountsQuery query, CancellationToken cancellationToken = default)
    {
        var accounts = await unitOfWork.BankAccounts.GetAllAsync(cancellationToken);
        return accounts.Select(account => account.ToDto()).ToList();
    }
}
