using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Wallet;

public class CreateWalletRequest
{
    [Required(ErrorMessage = "User ID is required.")]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Balance is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Balance must be positive.")]
    public decimal Balance { get; set; }
}