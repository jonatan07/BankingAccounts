using BankingAccounts.Application.Abstractions.Messaging;
using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Accounts.Mapping;
using BankingAccounts.Application.Accounts.Queries;
using BankingAccounts.Application.Accounts.Results;

namespace BankingAccounts.Application.Accounts.Handlers;

public sealed class GetBankAccountByIdQueryHandler(IUnitOfWork unitOfWork)
    : IQueryHandler<GetBankAccountByIdQuery, OperationResult<BankAccountDto>>
{
    public async Task<OperationResult<BankAccountDto>> HandleAsync(GetBankAccountByIdQuery query, CancellationToken cancellationToken = default)
    {
        var account = await unitOfWork.BankAccounts.GetByIdAsync(query.Id, cancellationToken);
        return account is null
            ? OperationResult<BankAccountDto>.Fail("Account not found.")
            : OperationResult<BankAccountDto>.Ok(account.ToDto());
    }
}
