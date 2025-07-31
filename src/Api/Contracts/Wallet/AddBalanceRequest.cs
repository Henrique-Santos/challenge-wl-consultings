using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Wallet;

public class AddBalanceRequest
{
    [Required(ErrorMessage = "User ID is required.")]
    public string UserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
}