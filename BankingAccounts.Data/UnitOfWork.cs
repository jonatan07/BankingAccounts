using BankingAccounts.Application.Abstractions.Persistence;
using BankingAccounts.Data.Repositories;

namespace BankingAccounts.Data;

public sealed class UnitOfWork(BankingDbContext dbContext) : IUnitOfWork
{
    private IBankAccountRepository? bankAccountRepository;

    public IBankAccountRepository BankAccounts =>
        bankAccountRepository ??= new BankAccountRepository(dbContext);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
