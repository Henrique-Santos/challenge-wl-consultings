using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult ReportError(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(p => p.Errors);

        return BadRequest(new
        {
            success = false,
            errors = errors.Select(e => e.ErrorMessage),
        });
    }

    protected IActionResult ReportError(string message)
    {
        return BadRequest(new
        {
            success = false,
            error = message
        });
    }

    protected IActionResult ReportSuccess(object? data = null)
    {
        return Ok(new
        {
            success = true,
            data
        });
    }
}