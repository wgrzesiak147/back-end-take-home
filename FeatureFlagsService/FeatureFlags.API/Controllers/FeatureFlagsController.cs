using FeatureFlags.Application.Models;
using FeatureFlags.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeatureFlagsController : ControllerBase
{
    private readonly IFeatureFlagService _service;

    public FeatureFlagsController(IFeatureFlagService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFlag(CreateFeatureFlagRequest request)
    {
        var result = await _service.CreateFeatureFlagAsync(request);
        return result ? Ok() : BadRequest("Flag already exists.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFlags()
    {
        var flags = await _service.GetAllFlagsAsync();
        return Ok(flags);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetFlag(string name)
    {
        var flag = await _service.GetFlagAsync(name);
        return flag != null ? Ok(flag) : NotFound();
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteFlag(string name)
    {
        var result = await _service.DeleteFlagAsync(name);
        return result ? Ok() : NotFound();
    }

    [HttpGet("{name}/audit")]
    public async Task<IActionResult> GetAuditLog(string name)
    {
        var logs = await _service.GetAuditLogAsync(name);
        return Ok(logs);
    }
}
