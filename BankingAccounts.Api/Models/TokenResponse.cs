namespace BankingAccounts.Api.Models;

public sealed record TokenResponse(string AccessToken, DateTime ExpiresAtUtc);
