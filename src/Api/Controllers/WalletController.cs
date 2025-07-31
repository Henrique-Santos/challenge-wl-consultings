using Api.Contracts.Wallet;
using Application.UseCases.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class WalletController : ApiController
{
    private readonly IAddBalanceUseCase _addBalanceUseCase;
    private readonly ICheckBalanceUseCase _checkBalanceUseCase;
    private readonly ICreateWalletUseCase _createWalletUseCase;

    public WalletController(IAddBalanceUseCase addBalanceUseCase, ICheckBalanceUseCase checkBalanceUseCase, ICreateWalletUseCase createWalletUseCase)
    {
        _addBalanceUseCase = addBalanceUseCase;
        _checkBalanceUseCase = checkBalanceUseCase;
        _createWalletUseCase = createWalletUseCase;
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

    [HttpPost("create-wallet")]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ReportError(ModelState);
        }

        try
        {
            await _createWalletUseCase.ExecuteAsync(request.UserId, request.Balance);
            return ReportSuccess("Wallet created successfully.");
        }
        catch (Exception ex)
        {
            return ReportError(ex.Message);
        }
    }
}