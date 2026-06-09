using BankingAccounts.Application.Abstractions.Messaging;
using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Application.Accounts.Commands;
using BankingAccounts.Application.Accounts.Results;

namespace BankingAccounts.Application.Accounts.Handlers;

public sealed class DeleteBankAccountCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteBankAccountCommand, OperationResult<bool>>
{
    public async Task<OperationResult<bool>> HandleAsync(DeleteBankAccountCommand command, CancellationToken cancellationToken = default)
    {
        var account = await unitOfWork.BankAccounts.GetByIdAsync(command.Id, cancellationToken);
        if (account is null)
        {
            return OperationResult<bool>.Fail("Account not found.");
        }

        unitOfWork.BankAccounts.Delete(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return OperationResult<bool>.Ok(true);
    }
}
