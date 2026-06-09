using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingAccounts.Data.Repositories;

public sealed class BankAccountRepository(BankingDbContext dbContext) : IBankAccountRepository
{
    public async Task<IReadOnlyList<BankAccount>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await dbContext.BankAccounts
            .AsNoTracking()
            .OrderBy(account => account.HolderName)
            .ToListAsync(cancellationToken);

    public Task<BankAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        dbContext.BankAccounts.FirstOrDefaultAsync(account => account.Id == id, cancellationToken);

    public Task<bool> AccountNumberExistsAsync(string accountNumber, Guid? excludedId = null, CancellationToken cancellationToken = default) =>
        dbContext.BankAccounts.AnyAsync(
            account => account.AccountNumber == accountNumber && (!excludedId.HasValue || account.Id != excludedId.Value),
            cancellationToken);

    public Task AddAsync(BankAccount account, CancellationToken cancellationToken = default) =>
        dbContext.BankAccounts.AddAsync(account, cancellationToken).AsTask();

    public void Update(BankAccount account) => dbContext.BankAccounts.Update(account);

    public void Delete(BankAccount account) => dbContext.BankAccounts.Remove(account);
}
