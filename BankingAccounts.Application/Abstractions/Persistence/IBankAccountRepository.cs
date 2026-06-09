using BankingAccounts.Domain.Entities;

namespace BankingAccounts.Application.Abstractions.Persistence;

public interface IBankAccountRepository
{
    Task<IReadOnlyList<BankAccount>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BankAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AccountNumberExistsAsync(string accountNumber, Guid? excludedId = null, CancellationToken cancellationToken = default);
    Task AddAsync(BankAccount account, CancellationToken cancellationToken = default);
    void Update(BankAccount account);
    void Delete(BankAccount account);
}
