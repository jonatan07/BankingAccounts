using BankingAccounts.Application.Abstractions.Messaging;
using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Application.Accounts.Commands;
using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Accounts.Mapping;
using BankingAccounts.Application.Accounts.Results;

namespace BankingAccounts.Application.Accounts.Handlers;

public sealed class UpdateBankAccountCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateBankAccountCommand, OperationResult<BankAccountDto>>
{
    public async Task<OperationResult<BankAccountDto>> HandleAsync(UpdateBankAccountCommand command, CancellationToken cancellationToken = default)
    {
        var accountNumber = command.AccountNumber.Trim().ToUpperInvariant();
        var currency = command.Currency.Trim().ToUpperInvariant();

        var account = await unitOfWork.BankAccounts.GetByIdAsync(command.Id, cancellationToken);
        if (account is null)
        {
            return OperationResult<BankAccountDto>.Fail("Account not found.");
        }

        if (await unitOfWork.BankAccounts.AccountNumberExistsAsync(accountNumber, command.Id, cancellationToken))
        {
            return OperationResult<BankAccountDto>.Fail("Account number already exists.");
        }

        account.AccountNumber = accountNumber;
        account.HolderName = command.HolderName.Trim();
        account.Balance = command.Balance;
        account.Currency = currency;
        account.IsActive = command.IsActive;
        account.UpdatedAtUtc = DateTime.UtcNow;

        unitOfWork.BankAccounts.Update(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult<BankAccountDto>.Ok(account.ToDto());
    }
}
