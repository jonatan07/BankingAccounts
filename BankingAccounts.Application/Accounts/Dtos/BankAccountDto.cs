namespace BankingAccounts.Application.Accounts.Dtos;

public sealed record BankAccountDto(
    Guid Id,
    string AccountNumber,
    string HolderName,
    decimal Balance,
    string Currency,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
    bool IsActive);
