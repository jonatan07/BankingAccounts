namespace BankingAccounts.Application.Accounts.Commands;

public sealed record UpdateBankAccountCommand(
    Guid Id,
    string AccountNumber,
    string HolderName,
    decimal Balance,
    string Currency,
    bool IsActive);
