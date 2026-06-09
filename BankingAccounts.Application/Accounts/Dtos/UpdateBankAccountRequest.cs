using System.ComponentModel.DataAnnotations;

namespace BankingAccounts.Application.Accounts.Dtos;

public sealed class UpdateBankAccountRequest
{
    [Required]
    [MaxLength(30)]
    public string AccountNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(120)]
    public string HolderName { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Balance { get; set; }

    [Required]
    [StringLength(3, MinimumLength = 3)]
    public string Currency { get; set; } = "USD";

    public bool IsActive { get; set; } = true;
}
