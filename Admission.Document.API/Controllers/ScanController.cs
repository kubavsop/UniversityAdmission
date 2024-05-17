using System.Net;
using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Document.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.Document.API.Controllers;

[Authorize]
public sealed class ScanController : BaseController
{
    private readonly IScanService _scanService;

    public ScanController(IScanService scanService)
    {
        _scanService = scanService;
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetScan(Guid id)
    {
        var result = await _scanService.GetScanAsync(UserId, id);
        return result.Match<IActionResult>(response => File(result.Value.Bytes, result.Value.Extension, result.Value.Name),
            ResultExtension.FailureResult);
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> DeleteScan(Guid id)
    {
        var result = await _scanService.DeleteScanAsync(UserId, id);
        return result.ToIActionResult();
    }
}