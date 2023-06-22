using MongoDB.Bson;

namespace Web.API.Models;

public class FeatureFlag
{
    public BsonObjectId Id { get; set; }
    public string? UserId { get; set; }
    public string ServiceName { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}