namespace BankingAccounts.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    IBankAccountRepository BankAccounts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
