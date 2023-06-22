using Microsoft.AspNetCore.Mvc;
using Web.API.Filters;
using Web.API.Services;

namespace Web.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[HttpExceptionFilter]
public class FeatureFlagsController : ControllerBase
{
    private readonly IFeatureFlagsService featureFlagsService;
    private readonly ILogger<FeatureFlagsController> logger;

    public FeatureFlagsController(IFeatureFlagsService featureFlagsService, ILogger<FeatureFlagsController> logger)
    {
        this.featureFlagsService = featureFlagsService;
        this.logger = logger;
    }

    [HttpGet("{serviceName}/{featureFlagName}")]
    public async Task<IActionResult> Get([FromRoute] string serviceName, [FromRoute] string featureFlagName, [FromQuery] string? userId = null)
    {
        var featureFlagValue = await featureFlagsService.GetFeatureFlagAsync(serviceName, featureFlagName, userId);
        logger.LogInformation($"Successfully got feature-flag '{featureFlagName} for userId '{userId}' and service name '{serviceName}'");
        return Ok(featureFlagValue);
    }

    [HttpPost("{serviceName}/{featureFlagName}")]
    public async Task<IActionResult> Post([FromRoute] string serviceName, [FromRoute] string featureFlagName, [FromBody] string value, [FromQuery] string? userId = null)
    {
        await featureFlagsService.SetFeatureFlagAsync(serviceName, featureFlagName, userId, value);
        logger.LogInformation($"Successfully added feature-flag '{featureFlagName} for userId '{userId}' and service name '{serviceName}'");
        return Ok();
    }

    [HttpPut("{serviceName}/{featureFlagName}")]
    public async Task<IActionResult> Put([FromRoute] string serviceName, [FromRoute] string featureFlagName, [FromBody] string value, [FromQuery] string? userId = null)
    {
        await featureFlagsService.SetFeatureFlagAsync(serviceName, featureFlagName, userId, value);
        logger.LogInformation($"Successfully updated feature-flag '{featureFlagName} for userId '{userId}' and service name '{serviceName}'");
        return Ok();
    }

    [HttpDelete("{serviceName}/{featureFlagName}")]
    public async Task<IActionResult> Delete([FromRoute] string serviceName, [FromRoute] string featureFlagName, [FromQuery] string? userId = null)
    {
        await featureFlagsService.DeleteFeatureFlagAsync(serviceName, featureFlagName, userId);
        logger.LogInformation($"Successfully deleted feature-flag '{featureFlagName} for userId '{userId}' and service name '{serviceName}'");
        return Ok();
    }
}