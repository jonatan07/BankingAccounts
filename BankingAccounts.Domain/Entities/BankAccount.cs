namespace BankingAccounts.Domain.Entities;

public sealed class BankAccount
{
    public Guid Id { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Currency { get; set; } = "USD";
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public bool IsActive { get; set; } = true;
}
