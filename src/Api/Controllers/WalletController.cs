using Api.Contracts.Balance;
using Application.UseCases.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class WalletController : ApiController
{
    private readonly IAddBalanceUseCase _addBalanceUseCase;
    private readonly ICheckBalanceUseCase _checkBalanceUseCase;

    public WalletController(IAddBalanceUseCase addBalanceUseCase, ICheckBalanceUseCase checkBalanceUseCase)
    {
        _addBalanceUseCase = addBalanceUseCase;
        _checkBalanceUseCase = checkBalanceUseCase;
    }

    [HttpGet("check-balance")]
    public async Task<IActionResult> CheckBalance(string userId)
    {
        try
        {
            var balance = await _checkBalanceUseCase.ExecuteAsync(userId);
            return ReportSuccess(new { balance });
        }
        catch (Exception ex)
        {
            return ReportError(ex.Message);
        }
    }

    [HttpPost("add-balance")]
    public async Task<IActionResult> AddBalance([FromBody] AddBalanceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ReportError(ModelState);
        }
        
        try
        {
            await _addBalanceUseCase.ExecuteAsync(request.UserId, request.Amount);
            return ReportSuccess("Balance added successfully.");
        }
        catch (Exception ex)
        {
            return ReportError(ex.Message);
        }
    }
}