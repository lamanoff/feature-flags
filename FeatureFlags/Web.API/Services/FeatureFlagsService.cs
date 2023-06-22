using Web.API.Repositories;

namespace Web.API.Services;

public class FeatureFlagsService : IFeatureFlagsService
{
    private readonly IFeatureFlagsRepository featureFlagsRepository;

    public FeatureFlagsService(IFeatureFlagsRepository featureFlagsRepository)
    {
        this.featureFlagsRepository = featureFlagsRepository;
    }

    public Task DeleteFeatureFlagAsync(string serviceName, string featureFlagName, string? userId)
    {
        return featureFlagsRepository.DeleteFeatureFlagAsync(serviceName, featureFlagName, userId);
    }

    public Task SetFeatureFlagAsync(string serviceName, string featureFlagName, string? userId, string value)
    {
        return featureFlagsRepository.SetFeatureFlagAsync(serviceName, featureFlagName, userId, value);
    }

    public Task<string?> GetFeatureFlagAsync(string serviceName, string featureFlagName, string? userId)
    {
        return featureFlagsRepository.GetFeatureFlagAsync(serviceName, featureFlagName, userId);
    }
}