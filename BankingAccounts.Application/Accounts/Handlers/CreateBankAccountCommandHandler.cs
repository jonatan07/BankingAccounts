using BankingAccounts.Application.Abstractions.Messaging;
using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Application.Accounts.Commands;
using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Accounts.Mapping;
using BankingAccounts.Application.Accounts.Results;
using BankingAccounts.Domain.Entities;

namespace BankingAccounts.Application.Accounts.Handlers;

public sealed class CreateBankAccountCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<CreateBankAccountCommand, OperationResult<BankAccountDto>>
{
    public async Task<OperationResult<BankAccountDto>> HandleAsync(CreateBankAccountCommand command, CancellationToken cancellationToken = default)
    {
        var accountNumber = command.AccountNumber.Trim().ToUpperInvariant();
        var currency = command.Currency.Trim().ToUpperInvariant();

        if (await unitOfWork.BankAccounts.AccountNumberExistsAsync(accountNumber, cancellationToken: cancellationToken))
        {
            return OperationResult<BankAccountDto>.Fail("Account number already exists.");
        }

        var account = new BankAccount
        {
            Id = Guid.NewGuid(),
            AccountNumber = accountNumber,
            HolderName = command.HolderName.Trim(),
            Balance = command.Balance,
            Currency = currency,
            CreatedAtUtc = DateTime.UtcNow,
            IsActive = true
        };

        await unitOfWork.BankAccounts.AddAsync(account, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult<BankAccountDto>.Ok(account.ToDto());
    }
}
