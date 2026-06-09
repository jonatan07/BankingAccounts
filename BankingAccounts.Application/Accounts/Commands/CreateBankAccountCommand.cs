namespace BankingAccounts.Application.Accounts.Commands;

public sealed record CreateBankAccountCommand(
    string AccountNumber,
    string HolderName,
    decimal Balance,
    string Currency);
