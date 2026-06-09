using BankingAccounts.Application.Abstractions.Messaging;
using BankingAccounts.Application.Accounts.Commands;
using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Accounts.Queries;
using BankingAccounts.Application.Accounts.Results;

namespace BankingAccounts.Application.Services;

public sealed class BankAccountService(
    IQueryHandler<GetAllBankAccountsQuery, IReadOnlyList<BankAccountDto>> getAllHandler,
    IQueryHandler<GetBankAccountByIdQuery, OperationResult<BankAccountDto>> getByIdHandler,
    ICommandHandler<CreateBankAccountCommand, OperationResult<BankAccountDto>> createHandler,
    ICommandHandler<UpdateBankAccountCommand, OperationResult<BankAccountDto>> updateHandler,
    ICommandHandler<DeleteBankAccountCommand, OperationResult<bool>> deleteHandler)
    : IBankAccountService
{
    public Task<IReadOnlyList<BankAccountDto>> GetAllAsync(CancellationToken cancellationToken = default) =>
        getAllHandler.HandleAsync(new GetAllBankAccountsQuery(), cancellationToken);

    public Task<OperationResult<BankAccountDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        getByIdHandler.HandleAsync(new GetBankAccountByIdQuery(id), cancellationToken);

    public Task<OperationResult<BankAccountDto>> CreateAsync(CreateBankAccountRequest request, CancellationToken cancellationToken = default) =>
        createHandler.HandleAsync(new CreateBankAccountCommand(request.AccountNumber, request.HolderName, request.Balance, request.Currency), cancellationToken);

    public Task<OperationResult<BankAccountDto>> UpdateAsync(Guid id, UpdateBankAccountRequest request, CancellationToken cancellationToken = default) =>
        updateHandler.HandleAsync(new UpdateBankAccountCommand(id, request.AccountNumber, request.HolderName, request.Balance, request.Currency, request.IsActive), cancellationToken);

    public Task<OperationResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        deleteHandler.HandleAsync(new DeleteBankAccountCommand(id), cancellationToken);
}
