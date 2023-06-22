using MongoDB.Driver;
using Web.API.Repositories;
using Web.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IFeatureFlagsRepository, FeatureFlagsRepository>();
builder.Services.AddSingleton<IFeatureFlagsService, FeatureFlagsService>();
builder.Services.AddSingleton<IMongoDatabase>(ctx =>
{
    var mongoClient = new MongoClient("");
    return mongoClient.GetDatabase("feature-flags");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();