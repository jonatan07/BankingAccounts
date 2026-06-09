using BankingAccounts.Application.Accounts.Dtos;
using BankingAccounts.Domain.Entities;

namespace BankingAccounts.Application.Accounts.Mapping;

public static class BankAccountMapper
{
    public static BankAccountDto ToDto(this BankAccount account) =>
        new(
            account.Id,
            account.AccountNumber,
            account.HolderName,
            account.Balance,
            account.Currency,
            account.CreatedAtUtc,
            account.UpdatedAtUtc,
            account.IsActive);
}
