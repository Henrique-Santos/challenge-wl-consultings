using Api.Contracts.Transfer;
using Application.UseCases.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class TransferController : ApiController
{
    private readonly IMakeTransferUseCase _makeTransfer;
    private readonly IListTransfersUseCase _listTransfersUseCase;

    public TransferController(IMakeTransferUseCase makeTransfer, IListTransfersUseCase listTransfersUseCase)
    {
        _makeTransfer = makeTransfer;
        _listTransfersUseCase = listTransfersUseCase;
    }

    [HttpGet("list-transfers")]
    public async Task<IActionResult> ListTransfers(string userId, DateTime? startDate = null, DateTime? endDate = null)
    {
        try
        {
            var transfers = await _listTransfersUseCase.ExecuteAsync(userId, startDate, endDate);
            return ReportSuccess(transfers);
        }
        catch (Exception ex)
        {
            return ReportError(ex.Message);
        }
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ReportError(ModelState);
        }

        try
        {
            await _makeTransfer.ExecuteAsync(request.FromUserId, request.ToUserId, request.Amount);
            return ReportSuccess("Transfer successful.");
        }
        catch (Exception ex)
        {
            return ReportError(ex.Message);
        }
    }
}