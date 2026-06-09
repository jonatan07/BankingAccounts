namespace BankingAccounts.Application.Accounts.Results;

public sealed record OperationResult<T>(bool Success, T? Value, string? Error)
{
    public static OperationResult<T> Ok(T value) => new(true, value, null);
    public static OperationResult<T> Fail(string error) => new(false, default, error);
}
