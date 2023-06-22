namespace Web.API.Services;

public interface IFeatureFlagsService
{
    Task DeleteFeatureFlagAsync(string serviceName, string featureFlagName, string? userId);
    Task SetFeatureFlagAsync(string serviceName, string featureFlagName, string? userId, string value);
    Task<string?> GetFeatureFlagAsync(string serviceName, string featureFlagName, string? userId);
}