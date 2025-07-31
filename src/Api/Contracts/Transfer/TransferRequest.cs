using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Transfer;

public class TransferRequest
{
    [Required(ErrorMessage = "From User ID is required.")]
    public string FromUserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "To User ID is required.")]
    public string ToUserId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
}