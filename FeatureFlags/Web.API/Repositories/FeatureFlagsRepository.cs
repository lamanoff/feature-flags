using System.Net;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using Web.API.Models;

namespace Web.API.Repositories;

public class FeatureFlagsRepository : IFeatureFlagsRepository
{
    private readonly IMongoCollection<FeatureFlag> featureFlagsCollection;

    public FeatureFlagsRepository(IMongoDatabase mongoDatabase)
    {
        featureFlagsCollection = mongoDatabase.GetCollection<FeatureFlag>("feature-flags");
    }

    public async Task DeleteFeatureFlagAsync(string serviceName, string featureFlagName, string? userId)
    {
        var filter = GetFilter(serviceName, featureFlagName, userId);
        var result = await featureFlagsCollection.DeleteOneAsync(filter);

        if (result.DeletedCount == 0)
            throw new HttpResponseException(HttpStatusCode.NotFound);
    }

    public async Task SetFeatureFlagAsync(string serviceName, string featureFlagName, string? userId, string value)
    {
        var filter = GetFilter(serviceName, featureFlagName, userId);
        var document =  await (await featureFlagsCollection.FindAsync(filter)).FirstOrDefaultAsync();

        if (document == null)
        {
            var featureFlag = new FeatureFlag
            {
                UserId = userId,
                ServiceName = serviceName,
                Name = featureFlagName,
                Value = value
            };
            await featureFlagsCollection.InsertOneAsync(featureFlag);
            return;
        }

        document.Value = value;

        await featureFlagsCollection.ReplaceOneAsync(filter, document);
    }

    public async Task<string?> GetFeatureFlagAsync(string serviceName, string featureFlagName, string? userId)
    {
        var filter = GetFilter(serviceName, featureFlagName, userId);
        var document =  await (await featureFlagsCollection.FindAsync(filter)).FirstOrDefaultAsync();

        if (document == null)
            throw new HttpResponseException(HttpStatusCode.NotFound);

        return document.Value;
    }

    private static FilterDefinition<FeatureFlag> GetFilter(string serviceName, string featureFlagName, string? userId)
    {
        return Builders<FeatureFlag>.Filter.And(
            Builders<FeatureFlag>.Filter.Eq("ServiceName", serviceName),
            Builders<FeatureFlag>.Filter.Eq("Name", featureFlagName),
            Builders<FeatureFlag>.Filter.Or(
                Builders<FeatureFlag>.Filter.Eq("UserId", userId),
                Builders<FeatureFlag>.Filter.Exists("UserId", false)
            )
        );
    }
}