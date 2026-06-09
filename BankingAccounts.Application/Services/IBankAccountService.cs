using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Application.Accounts.Results;

namespace BankingAccounts.Application.Services;

public interface IBankAccountService
{
    Task<IReadOnlyList<BankAccountDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OperationResult<BankAccountDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OperationResult<BankAccountDto>> CreateAsync(CreateBankAccountRequest request, CancellationToken cancellationToken = default);
    Task<OperationResult<BankAccountDto>> UpdateAsync(Guid id, UpdateBankAccountRequest request, CancellationToken cancellationToken = default);
    Task<OperationResult<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
